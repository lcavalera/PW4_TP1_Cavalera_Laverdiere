using AutoMapper;
using Events.Api.Entites.DTO;

namespace Events.Api.Entites
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Categorie, CategorieDTO>();
            CreateMap<CategorieDTO, Categorie>();
            CreateMap<Participation, ParticipationDTO>();
        }
    }
}
