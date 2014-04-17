using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
    }
}
