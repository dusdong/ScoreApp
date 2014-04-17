using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreApp.Infrastructure.Data
{
    public class ScoreRepository : IScoreRepository
    {
        public void Save(SaveScore score)
        {
            using (var database = DatabaseFactory.GetDatabase())
                database.Save<SaveScore>(score);
        }

        public Score GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Score> GetAll(bool timeUp = false)
        {
            throw new NotImplementedException();
        }

        public void TimedUp(int scoreId)
        {
            throw new NotImplementedException();
        }
    }
}
