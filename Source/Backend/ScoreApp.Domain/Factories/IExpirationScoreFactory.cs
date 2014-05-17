using System.Collections.Generic;

namespace ScoreApp.Domain.Factories
{
    public interface IExpirationScoreFactory
    {
        ExpirationScore Create(Score score);
        IEnumerable<ExpirationScore> Create(IEnumerable<Score> scores);
    }
}
