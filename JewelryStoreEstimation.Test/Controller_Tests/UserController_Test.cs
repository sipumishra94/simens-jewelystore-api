using API.Controllers;
using JewelryStoreEstimation.DTOs;
using JewelryStoreEstimation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JewelryStoreEstimation.Test
{
    public class UserController_Test
    {
        private readonly LoginDto _validLoginDTO;
        private readonly LoginDto _invalidLoginDTO;
        private readonly UserDto _userDTO;
        private readonly UserController _userController;

        public UserController_Test()
        {
            var userRepositoryMoq = new Mock<IUserRepository>();
            _validLoginDTO = new LoginDto
            {
                UserName = "Gobind",
                Password = "Password"
            };
            _invalidLoginDTO = new LoginDto
            {
                UserName = "Mishra",
                Password = "Password"
            };
            _userDTO = new UserDto
            {
                UserName = "Gobind",
                UserRole = "General"
            };

            userRepositoryMoq.Setup(r => r.LogInAsync(_validLoginDTO)).ReturnsAsync(_userDTO);
            userRepositoryMoq.Setup(r => r.GetDiscount()).Returns(2);
            userRepositoryMoq.Setup(r => r.LogInAsync(_invalidLoginDTO));

            _userController = new UserController(userRepositoryMoq.Object);
        }

        [Fact]
        public void Login_ValidCredentialProvided_ReturnUserDTO()
        {
            //Arrange

            //Act
            var result = _userController.Login(_validLoginDTO);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_userDTO, result.Result);
        }

        [Fact]
        public void Login_InValidCredentialProvided_ReturnsNull()
        {
            //Arrange

            //Act
            var result = _userController.Login(_validLoginDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Null(result.Result);
        }

        [Fact]
        public void Discount_Called_ReturnDiscount()
        {
            //Arrange

            //Act
            var result = _userController.Discount();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, result);
        }
    }
}
