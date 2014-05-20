using System;

namespace ScoreApp.Domain
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message, params object[] args)
            : base(string.Format(message, args)) { }
    }
}
