using System;

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
    }
}
