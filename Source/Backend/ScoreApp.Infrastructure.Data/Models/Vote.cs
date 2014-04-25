using System;

namespace ScoreApp.Infrastructure.Data.Models
{
    internal class Vote
    {
        public int ScoreId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public bool IsInFavor { get; set; }
    }
}
