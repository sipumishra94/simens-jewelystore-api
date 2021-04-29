using System.Linq;
using System.Threading.Tasks;
using JewelryStoreEstimation.Controllers;
using JewelryStoreEstimation.DTOs;
using JewelryStoreEstimation.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    public class UserController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.LogInAsync(loginDto);
            if (user == null)
                return NotFound("Wrong username or password");
            else
                return user;
        }

        [HttpGet("Discount")]
        public decimal Discount()
        {
            return _userRepository.GetDiscount();
        }    
    }
}