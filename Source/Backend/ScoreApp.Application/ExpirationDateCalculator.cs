using ScoreApp.Domain.Services;
using System;

namespace ScoreApp.Application
{
    public class ExpirationDateCalculator : IExpirationDateCalculator
    {
        public DateTime Calculate(DateTime createdDate)
        {
            //TODO: get this TimeSpan from admin parameters when admin parameters is done.
            var timeSpan = TimeSpan.FromMinutes(10);
            var expirationDate = createdDate.Add(timeSpan);
            return expirationDate > DateTime.Now ? expirationDate : DateTime.Now;
        }
    }
}
