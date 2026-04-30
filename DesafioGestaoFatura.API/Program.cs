using DesafioGestaoFatura.Application.DTO.Validations;
using DesafioGestaoFatura.Application.Interfaces;
using DesafioGestaoFatura.Application.Services;
using DesafioGestaoFatura.Infrastructure.Data;
using DesafioGestaoFatura.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers() ;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarFaturaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AdicionarItemValidation>();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFaturaRepository, FaturaRepository>();
builder.Services.AddScoped<FaturaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//middleware para capturar erros na service e retornar o json com a mensagem
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
