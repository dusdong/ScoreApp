using System;

namespace ScoreApp.Domain.Services
{
    public interface IExpirationDateCalculator
    {
        DateTime Calculate(DateTime createdDate);
    }
}
