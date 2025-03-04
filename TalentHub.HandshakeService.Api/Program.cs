using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TalentHub.HandshakeService.Api.Configurations;
using TalentHub.HandshakeService.Api.Extensions;
using TalentHub.HandshakeService.Application.Interfaces;
using TalentHub.HandshakeService.Application.Services;
using TalentHub.HandshakeService.Infrastructure.Abstractions;
using TalentHub.HandshakeService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;
using TalentHub.HandshakeService.Infrastructure.Data;
using TalentHub.HandshakeService.Infrastructure.EventHandlers;
using TalentHub.HandshakeService.Infrastructure.Models.Notification;
using TalentHub.HandshakeService.Infrastructure.Providers;
using TalentHub.HandshakeService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOptions<ApplicationConfiguration>()
    .BindConfiguration(nameof(ApplicationConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));
builder.Services.AddOptions<UserServiceClientConfiguration>()
    .BindConfiguration(nameof(UserServiceClientConfiguration));

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
         
        cfg.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VirtualHost, h =>
        {
            h.Username(rabbitMqConfiguration.Username);
            h.Password(rabbitMqConfiguration.Password);
        });
        
        cfg.Message<NotificationEvent>(ct => 
            ct.SetEntityName("notification_event"));
        
        cfg.Publish<NotificationEvent>(p =>
        {
            p.ExchangeType = ExchangeType.Direct;
        });
        
        cfg.Send<NotificationEvent>(s => 
            s.UseRoutingKeyFormatter(busCtx =>
                rabbitMqConfiguration.QueueName));
    });
});

builder.Services.AddScoped<IHandshakeRepository, HandshakeRepository>();

builder.Services.AddScoped<IHandshakeService, HandshakeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationEventFactory, NotificationEventFactory>();
builder.Services.AddScoped<IEventHandler<NotificationEvent>, NotificationEventHandler>();

builder.Services.AddControllers();
builder.Services.AddHttpClient<IUserService, UserService>((context, client) =>
{
    var configuration = context.GetService<IOptions<UserServiceClientConfiguration>>()
                        ?? throw new ConfigurationException($"Lack of '{nameof(UserServiceClientConfiguration)}' settings");
    
    var userServiceClientConfiguration = configuration.Value;
        
    client.BaseAddress = new Uri(userServiceClientConfiguration.Endpoint);
});

builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HandshakeDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Applying migrations...");
    dbContext.Database.Migrate();
    logger.LogInformation("Migrations applied successfully.");
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.Run();