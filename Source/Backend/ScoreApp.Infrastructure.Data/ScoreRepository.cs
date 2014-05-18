using NPoco;
using ScoreApp.Domain;
using ScoreApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScoreApp.Infrastructure.Data
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IWitnessRepository scoreWitnessRepository;

        public ScoreRepository(IUserRepository userRepository, IWitnessRepository scoreWitnessRepository)
        {
            this.userRepository = userRepository;
            this.scoreWitnessRepository = scoreWitnessRepository;
        }

        public void Save(SaveScore score)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                database.Save<SaveScore>(score);
                scoreWitnessRepository.Save(score.Id, score.Witnesses);
            }
        }

        public Score GetById(int id)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                var queryScore = database.SingleOrDefaultById<QueryScore>(id);
                if (queryScore == null)
                    return null;

                var users = GetUsers(queryScore);
                return queryScore.ToScore(users);
            }
        }

        private IEnumerable<User> GetUsers(params QueryScore[] queryScores)
        {
            var userIds = queryScores.Select(q => q.Candidate)
                    .Concat(queryScores.Select(q => q.Creator))
                    .Distinct();
            return userRepository.GetByIds(userIds.ToArray());
        }

        public IPagedResult<Score> GetAll(Pagination pagination, bool timeUp = false)
        {
            var scores = new Collection<Score>();
            using (var database = DatabaseFactory.GetDatabase())
            {
                var page = database.Page<QueryScore>(pagination.Page, pagination.ItemsPerPage, Sql.Builder.Where("TimeUp = @0", timeUp).OrderBy("Date DESC"));
                var users = GetUsers(page.Items.ToArray());

                foreach (var queryScore in page.Items)
                    scores.Add(queryScore.ToScore(users));

                return new PagedResult<Score>
                {
                    Items = scores,
                    ItemsPerPage = (int)page.ItemsPerPage,
                    CurrentPage = (int)page.CurrentPage,
                    TotalItems = (int)page.TotalItems
                };
            }
        }
    }
}
