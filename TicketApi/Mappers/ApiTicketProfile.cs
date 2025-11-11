using Application.Dtos;
using AutoMapper;
using Domain.Enums;
using TicketApi.Models.Requests;
using TicketApi.Models.Responses;

namespace TicketApi.Mappers;

public class ApiTicketProfile : Profile
{
    public ApiTicketProfile()
    {
        RegisterRequests();
        RegisterResponses();
    }

    private void RegisterRequests()
    {
        CreateMap<CreateTicketRequest, CreateTicketDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario));
        CreateMap<UpdateTicketRequest, UpdateTicketDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TicketStatus>(src.Estado)));
        CreateMap<PatchTicketRequest, PatchTicketDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.Estado)
                ? Enum.Parse<TicketStatus>(src.Estado)
                : (TicketStatus?)null));
    }
    private void RegisterResponses()
    {
        CreateMap<TicketDto, TicketResponse>()
                    .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Username))
                    .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => src.UpdatedAt))
                    .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
