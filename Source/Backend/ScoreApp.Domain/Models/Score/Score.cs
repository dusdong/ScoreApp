using System;
using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public class Score
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public User Creator { get; set; }
        public User Candidate { get; set; }
        public bool TimeUp { get; set; }
        public IEnumerable<Voter> Voters { get; set; }
        public IEnumerable<User> Witnesses { get; set; }
    }
}
