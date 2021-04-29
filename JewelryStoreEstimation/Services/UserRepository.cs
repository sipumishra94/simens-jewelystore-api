using JewelryStoreEstimation.Data;
using JewelryStoreEstimation.DTOs;
using JewelryStoreEstimation.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreEstimation.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public UserRepository(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }

        public decimal GetDiscount()
        {
            return _config.GetValue<Decimal>("Discount");
        }

        public async Task<UserDto> LogInAsync(LoginDto loginDto)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if (user == null)
                return null;
            else
                return new UserDto
                {
                    UserName = user.UserName,
                    UserRole = user.UserRole
                };
        }
    }
}
