using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using projekt.Repository;
using projekt.Database;
using projekt.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var provider = builder.Services.BuildServiceProvider();
//var conf = provider.GetRequiredService<IConfiguration>();
var db = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("db");

builder.Services.AddControllers();
builder.Services.AddSingleton<IAirplaneRepository, AirplaneRepository>();
//builder.Services.AddSingleton<IAirplane, Airplane>();

builder.Services.AddScoped<IAirplane>(_ => new Airplane(Guid.NewGuid(),"Start", "A", 6, new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } ));

builder.Services.AddSingleton(new DatabaseConfig { Name = db } );

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "projekt", Version = "v1" });
});

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "projekt v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();