using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.View;
using SquishFaceAPI.Service;

namespace SquishFaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagement _userManagement = null;

        public UserController(IOptions<AppConfig> appConfigs, IUserManagement userManagement)
        {
            _userManagement = userManagement;
            _userManagement.ResetGlobals();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _userManagement.GetAllUsers();

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(UserDetail userDetail)
        {
            if (
                userDetail == null || 
                string.IsNullOrEmpty(userDetail?.Email) ||
                string.IsNullOrEmpty(userDetail?.Name)
                ) 
                return BadRequest();

            var checkDuplicate = _userManagement.CheckExists(userDetail);

            if (checkDuplicate == null)
            {
                var result = _userManagement.AddUser(userDetail);

                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(result);
            }
            else
                return Conflict(checkDuplicate);
        }

        [HttpPut]
        public IActionResult Put(UserDetail userDetail)
        {
            if (userDetail == null) return BadRequest();

            var result = _userManagement.EditUser(userDetail);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("active")]
        public IActionResult GetActiveUsers()
        {
            var result = _userManagement.GetActiveUsers();

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserDetail userDetail)
        {
            if (
                userDetail == null ||
                string.IsNullOrEmpty(userDetail?.Password) ||
                string.IsNullOrEmpty(userDetail?.Name)
                )
                return BadRequest();

            return Ok(_userManagement.LoginUser(userDetail));
        }
    }
}
