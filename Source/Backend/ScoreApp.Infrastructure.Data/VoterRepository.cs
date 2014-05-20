﻿using NPoco;
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

        public IEnumerable<Voter> GetFromScore(int scoreId, bool? isInFavor)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                if (!database.Exists<QueryScore>(scoreId))
                    throw new EntityNotFoundException("Ponto com Id {0} não encontrado", scoreId);

                var query = Sql.Builder.Where("ScoreId = @0", scoreId);
                if (isInFavor.HasValue)
                    query.Where("IsInFavor = @0", isInFavor.Value);

                var votes = database.Fetch<Vote>(query);
                var users = GetUsers(votes.ToArray());
                return GetVoters(votes, users).ToList();
            }
        }

        public void SaveVote(Vote vote)
        {
            using (var database = DatabaseFactory.GetDatabase())
            {
                if (!database.Exists<QueryScore>(vote.ScoreId))
                    throw new EntityNotFoundException("Ponto com Id {0} não encontrado", vote.ScoreId);

                database.Save<Vote>(vote);
            }
        }
    }
}
