using System;

namespace ScoreApp.Domain
{
    public class ExpirationScore : Score
    {
        public ExpirationScore()
        {
            //The serialize needs this parameterless constructor.
        }

        public ExpirationScore(Score score)
        {
            Candidate = score.Candidate;
            Creator = score.Creator;
            Date = score.Date;
            Id = score.Id;
            Reason = score.Reason;
            TimeUp = score.TimeUp;
        }

        public double SecondsToExpire { get; set; }
    }
}