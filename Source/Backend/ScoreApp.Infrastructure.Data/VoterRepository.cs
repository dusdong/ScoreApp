using ScoreApp.Domain;
using ScoreApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreApp.Infrastructure.Data
{
    public class VoterRepository : IVoterRepository
    {
        private readonly IUserRepository userRepository;

        public VoterRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private IEnumerable<User> GetUsers(params Vote[] votes)
        {
            var userIds = votes.Select(q => q.User).Distinct();
            return userRepository.GetByIds(userIds.ToArray());
        }

        private IEnumerable<Voter> GetVoters(IEnumerable<Vote> votes, IEnumerable<User> users)
        {
            foreach (var vote in votes)
                yield return new Voter(users.First(u => u.Id == vote.User), vote.IsInFavor);
        }

        public IEnumerable<Voter> GetFromScore(int scoreId)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                if (!database.Exists<QueryScore>(scoreId))
                    throw new EntityNotFoundException("Ponto com Id {0} não encontrado", scoreId);

                var votes = database.FetchWhere<Vote>(s => s.ScoreId == scoreId);
                var users = GetUsers(votes.ToArray());
                return GetVoters(votes, users).ToList();
            }
        }
    }
}
