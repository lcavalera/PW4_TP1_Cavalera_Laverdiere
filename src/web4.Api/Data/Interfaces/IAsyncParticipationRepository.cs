using Events.Api.Entites;

namespace Events.Api.Data.Interfaces
{
    public interface IAsyncParticipationRepository : IAsyncRepository<Participation>
    {
        Task<Participation> GetByIdVerifyStatus(int id);
        bool SimulerVerifierStatus(Participation participation);
    }
}
