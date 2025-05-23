﻿using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.DAL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal_Asp.Net_MVC_.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<UserDTO> CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
                Password = userDto.Password,
                Salt = userDto.Salt,
                Role = userDto.Role,
                IsActive = userDto.IsActive
            };
            await Database.Users.Create(user);
            await Database.Save();

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Salt = user.Salt,
                Role = user.Role,
                IsActive = user.IsActive
            };

        }

        //public async Task CreateUser(UserDTO userDto)
        //{
        //    var user = new User
        //    {
        //        Id = userDto.Id,
        //        FirstName = userDto.FirstName,
        //        LastName = userDto.LastName,
        //        Login = userDto.Login,
        //        Password = userDto.Password,
        //        Salt = userDto.Salt,
        //        Role = userDto.Role,
        //        IsActive = userDto.IsActive
        //    };
        //    await Database.Users.Create(user);
        //    await Database.Save();
        //}

        public async Task UpdateUser(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
                Password = userDto.Password,
                Salt = userDto.Salt,
                Role = userDto.Role,
                IsActive = userDto.IsActive
            };
            Database.Users.Update(user);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);
            if (user == null)
                throw new ValidationException("Wrong user!", "");

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Salt = user.Salt,
                Role = user.Role,
                IsActive = user.IsActive,
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetList());
        }

        public async Task<IEnumerable<UserDTO>> GetInactiveUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetInactiveUsers());
        }

        public async Task ChangeActiveStatus(int id)
        {
            await Database.Users.ChangeActiveStatus(id);
            await Database.Save();
        }


        public async Task<bool> ExistsUser(int id)
        {
            return await Database.Users.Exists(id);
        }

        public async Task<UserDTO> GetUserByLogin(string login)
        {
            var user = await Database.Users.GetUserByLogin(login);
            if (user == null)
                throw new ValidationException("Wrong user!", "");

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Salt = user.Salt,
                Role = user.Role,
                IsActive = user.IsActive,
            };
        }

        public async Task<bool> AnyUsers()
        {
            return await Database.Users.AnyUsers();
        }

    }
}
