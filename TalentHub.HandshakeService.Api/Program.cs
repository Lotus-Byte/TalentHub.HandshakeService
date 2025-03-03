using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TalentHub.HandshakeService.Api.Configurations;
using TalentHub.HandshakeService.Api.Extensions;
using TalentHub.HandshakeService.Application.Interfaces;
using TalentHub.HandshakeService.Application.Services;
using TalentHub.HandshakeService.Infrastructure.Abstractions;
using TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;
using TalentHub.HandshakeService.Infrastructure.Data;
using TalentHub.HandshakeService.Infrastructure.Models.Notification;
using TalentHub.HandshakeService.Infrastructure.Providers;
using TalentHub.HandshakeService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<ApplicationConfiguration>()
    .BindConfiguration(nameof(ApplicationConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));

builder.Services.AddDbContext<HandshakeDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationConfiguration>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.RegisterMapper();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetService<IOptions<RabbitMqConfiguration>>()
                            ?? throw new ConfigurationException($"Lack of '{nameof(RabbitMqConfiguration)}' settings");

        var rabbitMqConfiguration = configuration.Value;

        cfg.Host(rabbitMqConfiguration.Host, rmqCfg =>
        {
            rmqCfg.Username(rabbitMqConfiguration.Username);
            rmqCfg.Password(rabbitMqConfiguration.Password);
        });

        cfg.Message<NotificationEvent>(ct => 
            ct.SetEntityName(rabbitMqConfiguration.QueueName));
        
        cfg.Send<NotificationEvent>(s => 
            s.UseRoutingKeyFormatter(_ => rabbitMqConfiguration.QueueName));
        
        cfg.Publish<NotificationEvent>(p =>
        {
            p.ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
            p.Durable = true;
            p.AutoDelete = false;
        });
    });
});

builder.Services.AddScoped<IHandshakeRepository, HandshakeRepository>();

builder.Services.AddScoped<IHandshakeService, HandshakeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationEventFactory, NotificationEventFactory>();

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();