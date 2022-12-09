using WeatherApp.Domain.Settings;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services.Weather;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection(nameof(WeatherSettings)));


builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
