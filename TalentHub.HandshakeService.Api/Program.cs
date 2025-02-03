using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TalentHub.HandshakeService.Api.Extensions;
using TalentHub.HandshakeService.Api.Settings;
using TalentHub.HandshakeService.Application.Interfaces;
using TalentHub.HandshakeService.Application.Services;
using TalentHub.HandshakeService.Infrastructure.Data;
using TalentHub.HandshakeService.Infrastructure.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<ApplicationSettings>()
    .BindConfiguration(nameof(ApplicationSettings));

builder.Services.AddDbContext<HandshakeDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationSettings>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.AddScoped<IHandshakeRepository, HandshakeRepository>();

builder.Services.AddScoped<HandshakeService, HandshakeService>();

builder.Services.AddControllers();
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