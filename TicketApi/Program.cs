
using Application;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Serilog;
using TicketApi.Helpers.ValidationHelper;
using TicketApi.Mappers;
using TicketApi.Middleware;
using TicketApi.Validators;

namespace TicketApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/ticketService-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddAutoMapper(cfg => { cfg.AllowNullCollections = true; },
           typeof(ApiTicketProfile).Assembly,
           typeof(Application.DependencyInjection).Assembly
           );
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        builder.Services.AddValidatorsFromAssemblyContaining<CreateTicketRequestValidator>();
        builder.Services.AddScoped<IValidationService, ValidationService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseMiddleware<ErrorMiddleware>();

        // Seed Database
        using (IServiceScope scope = app.Services.CreateScope())
        {
            TicketDbContext db = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
            db.Database.Migrate();
            InitialSeeder.Seed(db);
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
