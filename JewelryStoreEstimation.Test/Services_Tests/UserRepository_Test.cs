using API.Entities;
using Castle.Core.Configuration;
using JewelryStoreEstimation.Data;
using JewelryStoreEstimation.DTOs;
using JewelryStoreEstimation.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using Xunit;

namespace JewelryStoreEstimation.Test
{
    public class UserRepository_Test
    {
        private readonly UserDto _userDTO;
        private readonly AppUser _user;
        private readonly UserRepository _userRepository;
        private readonly LoginDto _validLoginDTO;
        private readonly LoginDto _invalidLoginDTO;

        public UserRepository_Test()
        {
            _userDTO = new UserDto
            {
                UserName = "Gobind",
                UserRole = "General"
            };

            _user = new AppUser
            {
                Id = 1,
                Password = "Password",
                UserName = "Gobind",
                UserRole = "General"
            };

            var configMoq = new Mock<IConfiguration>();
            var dataContextMoq = new Mock<DataContext>();
            dataContextMoq.Setup(x => x.Users.SingleOrDefaultAsync(x => x.UserName == "Gobind" && x.Password == "Password", It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(_user);
            configMoq.Setup(x => x.GetValue(typeof(Decimal), "Discount")).Returns(2);
        }


        [Fact]
        public void LogInAsync_ValidCredentialProvided_ReturnUserDTO()
        {
            //Arrange

            //Act
            var result = _userRepository.LogInAsync(_validLoginDTO);

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(_userDTO), JsonConvert.SerializeObject(result.Result));
        }

        [Fact]
        public void Login_InValidCredentialProvided_ReturnsNull()
        {
            //Arrange

            //Act
            var result = _userRepository.LogInAsync(_invalidLoginDTO);

            //Assert
            Assert.Null(result.Result);
        }

        [Fact]
        public void Discount_Called_ReturnDiscount()
        {
            //Arrange

            //Act
            var result = _userRepository.GetDiscount();

            //Assert
            Assert.Equal(2, result);
        }
    }
}
