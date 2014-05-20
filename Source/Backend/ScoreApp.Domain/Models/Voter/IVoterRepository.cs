using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IVoterRepository
    {
        IEnumerable<Voter> GetFromScore(int scoreId, bool? isInfavor);
        void SaveVote(Vote vote);
    }
}
