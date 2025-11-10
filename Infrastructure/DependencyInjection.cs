using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TicketDatabase")));

        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}
