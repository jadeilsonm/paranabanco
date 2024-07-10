using Aplication.UseCases;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;


// Injecting services
builder.Services.AddInfrastructureServices(config);

// Injecting use cases
builder.Services.AddUseCases();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAllOrigins",
               builder =>
               {
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
               });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(op =>
    op.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


InfraestructureInitializer.Initialize(app.Services);

app.Run();