using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data.Classes
{
    public class ParticipationAsyncRepository : AsyncRepository<Participation>, IAsyncParticipationRepository
    {
        public ParticipationAsyncRepository(EventsContext context) : base(context)
        {
        }

        public async Task<Participation> GetByIdVerifyStatus(int id)
        {
            return await _context.Set<Participation>().IgnoreQueryFilters().SingleOrDefaultAsync(e => e.Id == id);
        }
        public bool SimulerVerifierStatus(Participation participation)
        {
            if (!participation.EstValide)
            {
                participation.EstValide = new Random().Next(1, 10) > 5 ? true : false;
            }
            return participation.EstValide;
        }
    }
}