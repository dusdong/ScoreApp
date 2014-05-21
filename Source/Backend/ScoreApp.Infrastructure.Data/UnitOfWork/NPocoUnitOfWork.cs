using NPoco;
using ScoreApp.Domain.Services;

namespace ScoreApp.Infrastructure.Data
{
    public class NPocoUnitOfWork : IUnitOfWork
    {
        private readonly bool transactional;
        private bool done;

        public IDatabase Database { get; private set; }

        public NPocoUnitOfWork(IDatabase database, bool transactional = false)
        {
            this.transactional = transactional;

            Database = database;
            if (transactional)
                database.BeginTransaction();
        }

        public void Done()
        {
            done = true;
            Done(false);
        }

        private void Done(bool disposing)
        {
            if (disposing && transactional && !done)
                Database.AbortTransaction();
            else if (transactional)
                Database.CompleteTransaction();
        }

        public void Dispose()
        {
            if (Database != null)
            {
                Done(true);
                Database.Dispose();
            }
        }
    }
}
