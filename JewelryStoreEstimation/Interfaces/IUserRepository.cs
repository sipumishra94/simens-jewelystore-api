using JewelryStoreEstimation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreEstimation.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> LogInAsync(LoginDto loginDto);
        Decimal GetDiscount();
    }
}
