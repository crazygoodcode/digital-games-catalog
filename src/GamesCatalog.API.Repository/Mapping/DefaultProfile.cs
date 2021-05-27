using AutoMapper;

namespace GamesCatalog.API.Repository.Mapping
{
    public sealed class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Entities.User, Core.Models.User>()
                .ReverseMap()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Entities.Game, Core.Models.Game>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Released))
                .ReverseMap()
                .ForMember(dest => dest.GameId, opt => opt.Ignore());
        }
    }
}
