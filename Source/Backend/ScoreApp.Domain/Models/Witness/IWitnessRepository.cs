using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IWitnessRepository
    {
        IEnumerable<User> GetFromScore(int scoreId);
        void Save(int scoreId, IEnumerable<string> witnesses);
    }
}
