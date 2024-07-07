using Application;
using Core.Interfaces;
using Infrastructure;
using WebApi.BackgroudService;
using WebApi.Consumer;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddInfrastucture(config);

builder.Services.AddApplication();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IOnboardingConsumer, OnboardingConsumer>();

builder.Services.AddHostedService<ConsumerHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



app.Run();