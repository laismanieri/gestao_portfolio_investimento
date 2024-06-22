using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICliente, ClienteService>();
builder.Services.AddScoped<IInvestimento, InvestimentoService>();
builder.Services.AddScoped<IProdutoFinanceiro, ProdutoFinanceiroService>();
builder.Services.AddScoped<ITransacao, TransacaoService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
