using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IScoreRepository
    {
        void Save(SaveScore score);
        void TimedUp(int scoreId);
        Score GetById(int id);
        IEnumerable<Score> GetAll(bool timeUp = false);
    }
}
