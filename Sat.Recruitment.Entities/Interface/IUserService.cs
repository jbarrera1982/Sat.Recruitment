using Sat.Recruitment.Entities.Interface;

namespace Sat.Recruitment.Entities.Class
{
    public interface IUserService
    {
        Result CreateUser(IUser newUser);
        string Validate(IUser user);
    }
}