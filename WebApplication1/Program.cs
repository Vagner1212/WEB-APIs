﻿using Application.Services;
using Domain.Requests;
using Domain.Validators;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IValidator<BaseCarRequest>, CarValidator>();
builder.Services.AddSingleton<ICarRepository, CarRepository>();

var app = builder.Build();

// Middlewares
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();

// Redirect HTTPs
app.UseHttpsRedirection();

// Autorização
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();
