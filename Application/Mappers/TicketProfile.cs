using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketDto>();
    }
}
