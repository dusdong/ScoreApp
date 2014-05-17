using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;
using System.Collections.Generic;

namespace ScoreApp.Application
{
    public class ExpirationScoreFactory : IExpirationScoreFactory
    {
        private readonly ISettings settings;

        public ExpirationScoreFactory(ISettings settings)
        {
            this.settings = settings;
        }

        public ExpirationScore Create(Score score)
        {
            if (score == null)
                return null;

            var expiration = new ExpirationScore(score);
            expiration.SecondsToExpire = settings.ScoreExpirationTime.TotalSeconds; //TODO: later, calculate this with the score.Date + ExpirationTime.
            return expiration;
        }

        public IEnumerable<ExpirationScore> Create(IEnumerable<Score> scores)
        {
            foreach (var score in scores)
                yield return Create(score);
        }
    }
}
