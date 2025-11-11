
using Application;
using Infrastructure;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace TicketApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Seed Database
        using (IServiceScope scope = app.Services.CreateScope())
        {
            TicketDbContext db = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
            db.Database.Migrate();
            InitialSeeder.Seed(db);
        }

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
    }
}
