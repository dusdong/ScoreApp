using ScoreApp.Domain;
using System;

namespace ScoreApp.Api.Models
{
    public class ExpirationScore : Score
    {
        public ExpirationScore()
        {

        }

        public ExpirationScore(Score score)
        {
            Candidate = score.Candidate;
            Creator = score.Creator;
            Date = score.Date;
            Id = score.Id;
            Reason = score.Reason;
            TimeUp = score.TimeUp;
            Voters = score.Voters;
            Witnesses = score.Witnesses;
        }

        public DateTime ExpirationDate { get; set; }
    }
}