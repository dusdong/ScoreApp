using System;

namespace ScoreApp.Domain
{
    public class Vote
    {
        public int ScoreId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public bool IsInFavor { get; set; }
    }
}
