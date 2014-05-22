using NPoco;
using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using ScoreApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var toSave = new Collection<ScoreWitness>();
            var toDelete = new Collection<string>();
            var current = database.FetchWhere<ScoreWitness>(s => s.ScoreId == scoreId);

            foreach (var witness in witnesses)
            {
                if (!current.Any(c => c.Witness == witness))
                    toSave.Add(ScoreWitness.Create(scoreId, witness));
            }
            foreach (var cur in current)
            {
                if (!witnesses.Any(w => w == cur.Witness))
                    toDelete.Add(cur.Witness);
            }
            
            if (toDelete.Any())
                database.DeleteWhere<ScoreWitness>("ScoreId = @0 AND Witness IN (@1)", scoreId, string.Join(",", toDelete.ToArray()));
            if (toSave.Any())
                database.InsertBulk(toSave);
        }
    }
}
