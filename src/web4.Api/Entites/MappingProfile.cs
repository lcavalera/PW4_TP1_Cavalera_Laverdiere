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

            CreateMap<Evenement, EvenementDTO>();
        }
    }
}
