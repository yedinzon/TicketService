using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Seed;

public static class InitialSeeder
{
    public static void Seed(TicketDbContext context)
    {
        if (!context.Tickets.Any())
        {
            context.Tickets.AddRange(
                new Ticket("pepito01"), new Ticket("juanito02"), new Ticket("luisito03"), new Ticket("maria04"), new Ticket("carlitos05"),
                new Ticket("sofia06"), new Ticket("andres07"), new Ticket("valen08"), new Ticket("felipe09"), new Ticket("nataly10"),
                new Ticket("camilo11"), new Ticket("laura12"), new Ticket("sebastian13"), new Ticket("juliana14"), new Ticket("mateo15"),
                new Ticket("daniela16"), new Ticket("santiago17"), new Ticket("paula18"), new Ticket("david19"), new Ticket("karla20"),
                new Ticket("miguel21"), new Ticket("angelica22"), new Ticket("diego23"), new Ticket("isabella24"), new Ticket("alejandro25"),
                new Ticket("valeria26"), new Ticket("jose27"), new Ticket("camila28"), new Ticket("ricardo29"), new Ticket("angie30"),
                new Ticket("hector31"), new Ticket("carolina32"), new Ticket("lucas33"), new Ticket("melissa34"), new Ticket("tomas35"),
                new Ticket("samuel36"), new Ticket("leidy37"), new Ticket("cristian38"), new Ticket("adriana39"), new Ticket("manuel40"),
                new Ticket("lina41"), new Ticket("julio42"), new Ticket("stefany43"), new Ticket("edwin44"), new Ticket("karol45"),
                new Ticket("gustavo46"), new Ticket("nicolas47"), new Ticket("viviana48"), new Ticket("jorge49"), new Ticket("tatiana50")

            );
            context.SaveChanges();
        }
    }
}
