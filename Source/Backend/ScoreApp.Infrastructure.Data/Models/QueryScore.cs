using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreApp.Infrastructure.Data.Models
{
    internal class QueryScore
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public string Creator { get; set; }
        public string Candidate { get; set; }
        public bool TimeUp { get; set; }

        public Score ToScore(IEnumerable<User> users)
        {
            return new Score
            {
                Candidate = users.First(u => u.Id == Candidate),
                Creator = users.First(u => u.Id == Creator),
                Date = Date,
                Id = Id,
                Reason = Reason,
                TimeUp = TimeUp,
            };
        }
    }
}
