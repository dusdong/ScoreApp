using ScoreApp.Domain;
using ScoreApp.Domain.Services;
using ScoreApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreApp.Infrastructure.Data
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly IUserRepository userRepository;

        public ScoreRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Save(SaveScore score)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                database.Save<SaveScore>(score);
                //TODO: save witnesses 
            }
        }

        public Score GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Score> GetAll(bool timeUp = false)
        {
            var scores = new Collection<Score>();
            using (var database = DatabaseFactory.GetDatabase())
            {
                var queryScores = database.FetchWhere<QueryScore>(s => s.TimeUp == timeUp);
                var scoreIds = string.Join(",", queryScores.Select(s => s.Id).ToArray());
                var witnesses = database.Fetch<ScoreWitness>("WHERE ScoreId IN (@0)", scoreIds);
                var votes = database.Fetch<Vote>("WHERE ScoreId IN (@0)", scoreIds);

                var userIds = queryScores.Select(q => q.Candidate)
                    .Concat(queryScores.Select(q => q.Creator))
                    .Concat(witnesses.Select(w => w.Witness))
                    .Concat(votes.Select(v => v.User))
                    .Distinct();
                var users = userRepository.GetByIds(userIds.ToArray());

                foreach (var queryScore in queryScores)
                {
                    var score = new Score
                    {
                        Candidate = users.First(u => u.Id == queryScore.Candidate),
                        Creator = users.First(u => u.Id == queryScore.Creator),
                        Date = queryScore.Date,
                        Id = queryScore.Id,
                        Reason = queryScore.Reason,
                        TimeUp = queryScore.TimeUp,
                        Voters = GetVoters(votes.Where(v => v.ScoreId == queryScore.Id), users).ToList(),
                        Witnesses = witnesses.Where(w => w.ScoreId == queryScore.Id).Select(w => users.First(u => u.Id == w.Witness)).ToList()
                    };
                    scores.Add(score);
                }
            }

            return scores;
        }

        private IEnumerable<Voter> GetVoters(IEnumerable<Vote> votes, IEnumerable<User> users)
        {
            foreach (var vote in votes)
            {
                yield return new Voter(users.First(u => u.Id == vote.User))
                {
                    IsInFavor = vote.IsInFavor
                };
            }
        }

        public void TimedUp(int scoreId)
        {
            throw new NotImplementedException();
        }
    }
}
