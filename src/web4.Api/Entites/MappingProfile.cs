using AutoMapper;
using Events.Api.Entites.DTO;

namespace Events.Api.Entites
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Categorie, CategorieDTO>();
            CreateMap<CategorieDTO, Categorie>();

            CreateMap<Participation, ParticipationDTO>();
            CreateMap<ParticipationDTO, Participation>();

            CreateMap<Ville, VilleDTO>();
            //CreateMap<VilleDTO, Ville>();

            CreateMap<Evenement, EvenementDTO>();
                //.ForMember(dest => dest.CategorieIds, opt => opt.MapFrom(src => src.CategorieIds.Select(c => c.Id).ToList()));

            CreateMap<EvenementDTO, Evenement>();
                //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(id => new Categorie { Id = id })));

        }
    }
}
