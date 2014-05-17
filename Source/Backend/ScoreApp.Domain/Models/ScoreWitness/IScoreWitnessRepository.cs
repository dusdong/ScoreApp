using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IScoreWitnessRepository
    {
        IEnumerable<User> GetFromScore(int scoreId);
    }
}
