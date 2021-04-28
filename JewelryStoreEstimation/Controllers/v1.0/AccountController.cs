using System.Linq;
using System.Threading.Tasks;
using JewelryStoreEstimation.Controllers;
using JewelryStoreEstimation.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        public AccountController()
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           return null;
        }
    }
}