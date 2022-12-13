using WeatherApp.Domain.Settings;
using WeatherApp.Middleware;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services.Weather;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection(nameof(WeatherSettings)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Cors", builder =>
    {
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("Cors");

app.UseAuthorization();

app.UseMiddleware<ApiExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
