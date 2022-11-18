using System.Collections.Generic;

namespace Sat.Recruitment.Entities.Interface
{
    public interface IUserRepository
    {
        bool CreateUser(IUser user);
        List<IUser> GetUsers();
    }
}