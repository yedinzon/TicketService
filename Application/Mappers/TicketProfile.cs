using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

/// <summary>
/// Configurations for mapping Ticket entity
/// </summary>
public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketDto>();
    }
}
