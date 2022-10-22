using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<LineLoginConfig>(builder.Configuration.GetSection("LineLogin"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/LineLoginSetting", (IOptions<LineLoginConfig> lineLoginConfigOptions) => new
{
    lineLoginConfigOptions.Value.ClientId, 
    lineLoginConfigOptions.Value.ClientSecret,
    DateTime.Now
})
.WithName("GetWeatherForecast");

app.Run();

public class LineLoginConfig
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}