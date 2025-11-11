namespace TicketApi.Models.Requests;

public record PagedRequest(int PageNumber = 1, int PageSize = 10);
