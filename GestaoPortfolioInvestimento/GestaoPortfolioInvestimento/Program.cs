using GestaoPortfolioInvestimento.Data;
using GestaoPortfolioInvestimento.Interfaces;
using GestaoPortfolioInvestimento.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
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
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<ICliente, ClienteService>();
        builder.Services.AddScoped<IInvestimento, InvestimentoService>();
        builder.Services.AddScoped<IProdutoFinanceiro, ProdutoFinanceiroService>();
        builder.Services.AddScoped<ITransacao, TransacaoService>();
        builder.Services.AddTransient<ExtratoService>();
        builder.Services.AddTransient<EmailService>();
        builder.Services.AddSwaggerGen();

        // Recuperar a chave da API do SendGrid a partir de variáveis de ambiente
        string sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        if (string.IsNullOrEmpty(sendGridApiKey))
        {
            Console.WriteLine("SendGrid API Key is not set in the environment variables.");
            return;
        }

        var emailService = new EmailService(sendGridApiKey);

        await emailService.EnviarEmail("laismanieri@alunos.utfpr.edu.br", "Assunto do E-mail", "Corpo do E-mail");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

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
    }
}