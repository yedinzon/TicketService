using Application.Dtos;
using AutoMapper;
using TicketApi.Models.Requests;
using TicketApi.Models.Responses;

namespace TicketApi.Mappers;

public class ApiTicketProfile : Profile
{
    public ApiTicketProfile()
    {
        CreateMap<CreateTicketRequest, CreateTicketDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario));

        CreateMap<TicketDto, TicketResponse>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
