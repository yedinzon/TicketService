using Application.Dtos;
using AutoMapper;
using TicketApi.Models.Requests;

namespace TicketApi.Mappers;

public class ApiTicketProfile : Profile
{
    public ApiTicketProfile()
    {
        CreateMap<CreateTicketRequest, CreateTicketDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario));
    }
}
