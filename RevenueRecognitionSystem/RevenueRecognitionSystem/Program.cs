using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.DAL.Repositories;
using RevenueRecognitionSystem.Model;
using RevenueRecognitionSystem.Services;

public class Program
{
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<ISoftwareOrderRepository, SoftwareOrderRepository>();
        builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IRevenueService, RevenueService>();
        builder.Services.AddScoped<ISoftwareOrdeService, SoftwareOrderService>();
        builder.Services.AddScoped<RevenueRecognitionContext>();
        
        var app = builder.Build();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}