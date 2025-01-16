using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TalentHub.InterdocService.Api.Extensions;
using TalentHub.InterdocService.Api.Settings;
using TalentHub.InterdocService.Application.Interfaces;
using TalentHub.InterdocService.Application.Services;
using TalentHub.InterdocService.Infrastructure.Data;
using TalentHub.InterdocService.Infrastructure.Interfaces;
using TalentHub.InterdocService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<ApplicationSettings>()
    .BindConfiguration(nameof(ApplicationSettings));

builder.Services.AddDbContext<InterdocDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationSettings>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.AddScoped<IInterdocRepository, InterdocRepository>();

builder.Services.AddScoped<IInterdocService, InterdocService>();

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