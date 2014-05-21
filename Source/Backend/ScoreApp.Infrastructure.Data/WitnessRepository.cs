using NPoco;
using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using ScoreApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScoreApp.Infrastructure.Data
{
    public class WitnessRepository : IWitnessRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IDatabase database;

        public WitnessRepository(IDatabaseFactory databaseFactory, IUserRepository userRepository)
        {
            this.database = databaseFactory.Get();
            this.userRepository = userRepository;
        }

        private IEnumerable<User> GetUsers(params ScoreWitness[] scoreWitnesses)
        {
            var userIds = scoreWitnesses.Select(q => q.Witness).Distinct();
            return userRepository.GetByIds(userIds.ToArray());
        }

        public IEnumerable<User> GetFromScore(int scoreId)
        {
            if (!database.Exists<QueryScore>(scoreId))
                throw new EntityNotFoundException("Ponto com Id {0} não encontrado", scoreId);

            var witnesses = database.FetchWhere<ScoreWitness>(s => s.ScoreId == scoreId);
            return GetUsers(witnesses.ToArray());
        }

        public void Save(int scoreId, IEnumerable<string> witnesses)
        {
            //TODO: insert or update accordingly...
        }
    }
}
