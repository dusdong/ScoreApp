using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        IEnumerable<User> GetByIds(string[] ids);
        User GetById(string id);
        User GetByToken(string token);
    }
}
