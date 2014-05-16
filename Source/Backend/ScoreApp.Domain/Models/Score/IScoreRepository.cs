using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IScoreRepository
    {
        void Save(SaveScore score);
        Score GetById(int id);
        PagedResult<Score> GetAll(Pagination pagination, bool timeUp = false);
    }
}
