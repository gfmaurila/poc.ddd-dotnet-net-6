using Bogus;
using Bogus.DataSets;
using Demo.Application.Demo.DTO.DTO;
using Demo.Application.Demo.DTO.Request;
using Demo.Domain.Entities;
using System.Collections.Generic;

namespace Demo.Tests.Fixtures
{
    public class UserFixture
    {

        public static User CreateValidUser()
        {
            return new User(
                name: new Name().FirstName(),
                email: new Internet().Email(),
                password: "@A147d258g369");
        }

        public static List<User> CreateListValidUser(int limit = 5)
        {
            var list = new List<User>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValidUser());

            return list;
        }

        public static UserDTO CreateValidUserDTO(bool newId = false)
        {
            return new UserDTO
            {
                Id = newId ? new Randomizer().Int(0, 1000) : 0,
                Name = new Name().FirstName(),
                Email = new Internet().Email(),
                Password = "@A147d258g369"
            };
        }

        public static UserDTO CreateInvalidUserDTO()
        {
            return new UserDTO
            {
                Id = 0,
                Name = "",
                Email = "",
                Password = ""
            };
        }

        public static CreateUserRequest CreateValidCreateUser()
        {
            return new CreateUserRequest
            {
                Name = new Name().FirstName(),
                Email = new Internet().Email(),
                Password = "@A147d258g369"
            };
        }

        public static CreateUserRequest CreateValidCreateUser(string pass)
        {
            return new CreateUserRequest
            {
                Name = new Name().FirstName(),
                Email = new Internet().Email(),
                Password = pass
            };
        }

        public static CreateUserRequest CreateInvalidCreateUserName()
        {
            return new CreateUserRequest
            {
                Name = "",
                Email = new Internet().Email(),
                Password = new Internet().Password()
            };
        }

        public static CreateUserRequest CreateInvalidCreateUserEmailNull()
        {
            return new CreateUserRequest
            {
                Name = new Name().FirstName(),
                Email = "",
                Password = "@A147d258g369"
            };
        }

        public static CreateUserRequest CreateInvalidCreateUserEmail()
        {
            return new CreateUserRequest
            {
                Name = new Name().FirstName(),
                Email = "teste",
                Password = "@A147d258g369"
            };
        }

        public static CreateUserRequest CreateInvalidCreateUserPasswordNull()
        {
            return new CreateUserRequest
            {
                Name = new Name().FirstName(),
                Email = new Internet().Email(),
                Password = ""
            };
        }

    }
}
