using Domain.Enums;

namespace TicketApi.Helpers;

public static class EnumValidators
{
    public static bool BeAValidTicketStatus(string status)
    {
        return Enum.TryParse(typeof(TicketStatus), status, ignoreCase: false, out _);
    }
}
