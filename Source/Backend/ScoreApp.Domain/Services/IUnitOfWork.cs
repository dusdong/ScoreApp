using NPoco;
using System;

namespace ScoreApp.Domain.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IDatabase Database { get; }
        void Done();
    }
}
