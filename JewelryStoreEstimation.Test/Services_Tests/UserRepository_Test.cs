using API.Entities;
using JewelryStoreEstimation.Data;
using JewelryStoreEstimation.DTOs;
using JewelryStoreEstimation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestSupport.EfHelpers;
using Xunit;

namespace JewelryStoreEstimation.Test
{
    public class UserRepository_Test
    {
        private readonly UserDto _userDTO;
        private readonly AppUser _user;
        private  UserRepository _userRepository;
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
        }


        [Fact]
        public void LogInAsync_ValidCredentialProvided_ReturnUserDTO()
        {

            //SETUP
            var options = SqliteInMemory.CreateOptions<DataContext>();
            using var context = new DataContext(options);
            context.Database.EnsureCreated();
            context.Add(_user);
            context.SaveChanges();

            var inMemorySettings = new Dictionary<string, String> { { "Discount", "2" } };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _userRepository = new UserRepository(context, configuration);



            //Act
            var result = _userRepository.LogInAsync(_validLoginDTO).Result;

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(_userDTO), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void Login_InValidCredentialProvided_ReturnsNull()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<DataContext>();
            using var context = new DataContext(options);
            context.Database.EnsureCreated();
            context.Add(_user);
            context.SaveChanges();

            var inMemorySettings = new Dictionary<string, String> { { "Discount", "2" } };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _userRepository = new UserRepository(context, configuration);

            //Act
            var result = _userRepository.LogInAsync(_invalidLoginDTO);

            //Assert
            Assert.Null(result.Result);
        }

        [Fact]
        public void Discount_Called_ReturnDiscount()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<DataContext>();
            using var context = new DataContext(options);
            context.Database.EnsureCreated();
            context.Add(_user);
            context.SaveChanges();

            var inMemorySettings = new Dictionary<string, String> { { "Discount", "2" } };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _userRepository = new UserRepository(context, configuration);

            //Act
            var result = _userRepository.GetDiscount();

            //Assert
            Assert.Equal(2, result);
        }
    }
}
