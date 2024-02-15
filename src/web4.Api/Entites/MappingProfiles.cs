using AutoMapper;
using Events.Api.Entites.DTO;

namespace Events.Api.Entites
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categorie, CategorieDTO>();
            CreateMap<Participation, ParticipationDTO>();
            CreateMap<Ville, VilleDTO>();

            CreateMap<Evenement, EvenementDTO>()
                .ForMember(dest => dest.CategoriesIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id).ToList()));



            //CreateMap<EvenementDTO, Evenement>()
            //    .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoriesIds));
        }
    }
}
