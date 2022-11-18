using Sat.Recruitment.Entities.Interface;
using System.Collections.Generic;

namespace Sat.Recruitment.Entities.Class
{
    public class UserService : IUserService
    {
        private readonly ILogService _logger;
        private readonly IUserRepository _repository;

        public UserService(ILogService logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public string Validate(IUser user)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(user.Name))
                //Validate if Name is null
                errors.Add("The name is required");
            if (string.IsNullOrEmpty(user.Email))
                //Validate if Email is null
                errors.Add(" The email is required");
            if (string.IsNullOrEmpty(user.Address))
                //Validate if Address is null
                errors.Add("The address is required");
            if (string.IsNullOrEmpty(user.Phone))
                //Validate if Phone is null
                errors.Add(" The phone is required");
            return string.Join(',', errors);
        }

        public Result CreateUser(IUser newUser)
        {
            var userList = _repository.GetUsers();
            var isDuplicated = false;
            string message;
            foreach (var user in userList)
            {
                if (user.Email == newUser.Email || user.Phone == newUser.Phone || (user.Name == newUser.Name && user.Address == newUser.Address))
                {
                    isDuplicated = true;
                }

                if (isDuplicated)
                {
                    message = "The user is duplicated";
                    _logger.Info(message);
                    return new Result()
                    {
                        IsSuccess = false,
                        Message = message
                    };
                }
            }
            _repository.CreateUser(newUser);
            message = "User Created";
            _logger.Info(message);
            return new Result()
            {
                IsSuccess = true,
                Message = message
            };
        }
    }
}
