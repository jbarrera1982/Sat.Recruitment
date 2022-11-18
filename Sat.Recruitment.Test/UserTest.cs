using Sat.Recruitment.Api.Class;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Entities.Class;
using Sat.Recruitment.Repository.Class;
using System;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        [Fact]
        public void CreateUser_InvalidUParameters_ReturnsInvalid()
        {
            // Arrange                        
            var logger = new LogService();
            var repository = new UserRepository();
            var userController = new UsersController(new LogService(), new UserService(logger, repository));

            var user = new User();
            user.Name = "Mike";

            // Act
            var result = userController.CreateUser(user).Result;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Message);
        }
        [Fact]
        public void CreateUser_CorrectParameters_ReturnsSuccess()
        {
            // Arrange
            var logger = new LogService();
            var repository = new UserRepository();
            var userController = new UsersController(new LogService(), new UserService(logger, repository));

            var user = new User();
            var tmpName = DateTime.Now.Millisecond;
            user.Name = $"Mike{tmpName}";
            user.Email = $"mike{tmpName}@gmail.com";
            user.Address = "Av. Juan G";
            user.Phone = $"+349 1122354{tmpName}";
            user.UserType = "Normal";
            user.Money = 124;

            // Act
            var result = userController.CreateUser(user).Result;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public void CreateUser_Duplicated_ReturnsDuplicated()
        {
            // Arrange
            var logger = new LogService();
            var repository = new UserRepository();
            var userController = new UsersController(new LogService(), new UserService(logger, repository));

            var user = new User();
            user.Name = "Agustina";
            user.Email = "Agustina@gmail.com";
            user.Address = "Av. Juan G";
            user.Phone = "+349 1122354215";
            user.UserType = "Normal";
            user.Money = 124;

            // Act
            var result = userController.CreateUser(user).Result;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
        }
    }
}
;