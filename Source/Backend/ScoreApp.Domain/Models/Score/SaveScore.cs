using System;
using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public class SaveScore
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public string Creator { get; set; }
        public string Candidate { get; set; }
        public bool TimeUp { get; set; }
        public IEnumerable<string> Witnesses { get; set; }
    }
}
