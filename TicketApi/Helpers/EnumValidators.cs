using Domain.Enums;

namespace TicketApi.Helpers;

public static class EnumValidators
{
    /// <summary>
    /// Validates if the provided status string corresponds to a valid TicketStatus enum value.
    /// </summary>
    /// <param name="status">The status string to validate.</param>
    /// <returns>True if valid, otherwise false.</returns>
    public static bool BeAValidTicketStatus(string status)
    {
        return Enum.TryParse(typeof(TicketStatus), status, ignoreCase: false, out _);
    }
}
