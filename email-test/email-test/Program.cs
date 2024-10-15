using email_test.Services.Implementation;
using email_test.Services.Interfaces;

namespace email_test;

public static class Program
{
    public static Task<WebApplication> InitializeApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        
        builder.Services.AddScoped<IEmailService, EmailService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        
        app.MapControllers();

        return Task.FromResult(app);
    }
    
    public static async Task Main(string[] args)
    {
        await (await InitializeApplication(args)).RunAsync();
    }
}