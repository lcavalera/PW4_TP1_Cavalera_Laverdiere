using AutoMapper;
using Events.Api.Entites.DTO;

namespace Events.Api.Entites.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Categorie, CategorieDTO>();
            CreateMap<Participation, ParticipationDTO>();
            CreateMap<Ville, VilleDTO>();
            CreateMap<Evenement, EvenementDTO>();

        }
    }
}
