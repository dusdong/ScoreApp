using ScoreApp.Domain;
using ScoreApp.Domain.Factories;

namespace ScoreApp.Infrastructure.Data
{
    /// <summary>
    /// This is a decorator to create ExpirationScore on top of Score.
    /// </summary>
    public class ExpirationScoreRepository : IScoreRepository
    {
        private readonly IScoreRepository repository;
        private readonly IExpirationScoreFactory expirationScoreFactory;

        public ExpirationScoreRepository(IScoreRepository repository, IExpirationScoreFactory expirationScoreFactory)
        {
            this.repository = repository;
            this.expirationScoreFactory = expirationScoreFactory;
        }

        public Score Save(SaveScore score)
        {
            return repository.Save(score);
        }

        public Score GetById(int id)
        {
            var score = repository.GetById(id);
            return expirationScoreFactory.Create(score);
        }

        public IPagedResult<Score> GetAll(Pagination pagination, bool timeUp = false)
        {
            var result = repository.GetAll(pagination, timeUp);
            var expirationScores = expirationScoreFactory.Create(result.Items);
            return result.To(expirationScores);
        }
    }
}
