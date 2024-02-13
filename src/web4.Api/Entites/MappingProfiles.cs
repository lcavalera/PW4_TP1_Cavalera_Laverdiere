using AutoMapper;
using Events.Api.Entites.DTO;

namespace Events.Api.Entites
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Categorie, CategorieDTO>();
            CreateMap<Participation, ParticipationDTO>();
            CreateMap<Ville, VilleDTO>();
            CreateMap<Evenement, EvenementDTO>();
        }
    }
}
