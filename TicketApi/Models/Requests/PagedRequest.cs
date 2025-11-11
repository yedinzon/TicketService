namespace TicketApi.Models.Requests;

/// <summary>
/// Request model for paginated data retrieval
/// </summary>
public record PagedRequest(int PageNumber = 1, int PageSize = 10);
