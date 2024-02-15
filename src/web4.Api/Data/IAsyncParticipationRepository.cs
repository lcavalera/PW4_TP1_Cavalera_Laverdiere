using Events.Api.Entites;

namespace Events.Api.Data
{
    public interface IAsyncParticipationRepository : IAsyncRepository<Participation>
    {
        Task<Participation> GetByIdVerifyStatus(int id);
    }
}
