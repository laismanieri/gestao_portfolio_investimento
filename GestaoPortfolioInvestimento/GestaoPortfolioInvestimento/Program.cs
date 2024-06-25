using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Jobs;
using GestaoPortfolioInvestimento.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.

// Configurar controladores com serialização de enums como strings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICliente, ClienteService>();
builder.Services.AddScoped<IInvestimento, InvestimentoService>();
builder.Services.AddScoped<IProdutoFinanceiro, ProdutoFinanceiroService>();
builder.Services.AddScoped<ITransacao, TransacaoService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddTransient<ExtratoService>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<QuartzHostedService>();

builder.Services.AddSwaggerGen();

// Add Quartz.NET services
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

// Register EnviarEmailJob as Transient
builder.Services.AddTransient<EnviarEmailJob>();

// Register QuartzHostedService as Hosted Service
builder.Services.AddHostedService<QuartzHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();