using ScoreApp.Domain;
using ScoreApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScoreApp.Infrastructure.Data
{
    public class WitnessRepository : IWitnessRepository
    {
        private readonly IUserRepository userRepository;

        public WitnessRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private IEnumerable<User> GetUsers(params ScoreWitness[] scoreWitnesses)
        {
            var userIds = scoreWitnesses.Select(q => q.Witness).Distinct();
            return userRepository.GetByIds(userIds.ToArray());
        }

        public IEnumerable<User> GetFromScore(int scoreId)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                var witnesses = database.FetchWhere<ScoreWitness>(s => s.ScoreId == scoreId);
                return GetUsers(witnesses.ToArray());
            }
        }

        public void Save(int scoreId, IEnumerable<string> witnesses)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                //TODO: insert or update accordingly...
            }
        }
    }
}
