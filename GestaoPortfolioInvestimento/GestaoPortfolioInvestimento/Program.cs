using InvestmentPortfolioManagement.Application.Interfaces;
using InvestmentPortfolioManagement.Application.Services;
using InvestmentPortfolioManagement.Infrastructure;
using InvestmentPortfolioManagement.Infrastructure.Jobs;
using InvestmentPortfolioManagement.Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

SqliteConnection? inMemorySqliteConnection = null;

builder.Services.AddDbContext<DataContext>(options =>
{
    if (environment.IsDevelopment() || environment.IsEnvironment("Testing"))
    {
        if (inMemorySqliteConnection == null)
        {
            inMemorySqliteConnection = new SqliteConnection("Data Source=:memory:;Cache=Shared");
            inMemorySqliteConnection.Open();
        }

        options.UseSqlite(inMemorySqliteConnection);
    }
    else
    {
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(cs);
    }
});

// Add services to the container.

// Configurar controladores com serialização de enums como strings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<IFinancialProductService, FinancialProductService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddTransient<IEmailNotificationService, EmailService>();

builder.Services.AddTransient<StatementService>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<QuartzHostedService>();

builder.Services.AddSwaggerGen();

// Add Quartz.NET services
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

// Register EnviarEmailJob as Transient
builder.Services.AddTransient<SendUpcomingInvestmentsEmailJob>();

// Register QuartzHostedService as Hosted Service
builder.Services.AddHostedService<QuartzHostedService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();  
}

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