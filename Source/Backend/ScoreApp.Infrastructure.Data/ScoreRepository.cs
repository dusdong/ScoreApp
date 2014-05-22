using NPoco;
using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
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
        private readonly IDatabase database;

        public ScoreRepository(IDatabaseFactory databaseFactory, IUserRepository userRepository, IWitnessRepository scoreWitnessRepository)
        {
            this.database = databaseFactory.Get();
            this.userRepository = userRepository;
            this.scoreWitnessRepository = scoreWitnessRepository;
        }

        public Score Save(SaveScore score)
        {
            database.Save<SaveScore>(score);
            scoreWitnessRepository.Save(score.Id, score.Witnesses);
            return GetById(score.Id);
        }

        public Score GetById(int id)
        {
            var queryScore = database.SingleOrDefaultById<QueryScore>(id);
            if (queryScore == null)
                return null;

            var users = GetUsers(queryScore);
            return queryScore.ToScore(users);
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
