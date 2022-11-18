using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Entities.Class;
using Sat.Recruitment.Entities.Interface;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogService _logger;
        private readonly IUserService _userService;        
        
        public UsersController(ILogService logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;            
        }

        [HttpPost]
        [Route("/create-user")]
        public Task<Result> CreateUser([FromBody] IUser inputUser)
        {
            var errors = _userService.Validate(inputUser);
            if (!string.IsNullOrEmpty(errors))
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Message = errors
                });

            try
            {
                var result = _userService.CreateUser(inputUser);
                return Task.FromResult(result);
            }
            catch (System.Exception e)
            {
                _logger.Error(e);
                return Task.FromResult(new Result()
                {
                    IsSuccess = false,
                    Message = e.Message
                });
            }
        }        
    }   
}
