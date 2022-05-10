using AutoMapper;
using Bogus;
using Bogus.DataSets;
using Demo.API.Controllers;
using Demo.Application.Demo.DTO.DTO;
using Demo.Application.Demo.DTO.Request;
using Demo.Application.Demo.DTO.Response;
using Demo.Domain.Core.Interfaces.Repositorys;
using Demo.Domain.Core.Interfaces.Services;
using Demo.Domain.Entities;
using Demo.Domain.Services.Services;
using Demo.Tests.Configurations.AutoMapper;
using Demo.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PocNugetEncryptDecrypt.Interfaces;
using Xunit;

namespace Demo.Tests.Projects.DemoAPI
{
    public class UserControllerTests
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        //Mocks
        private readonly Mock<IUserRepository> _repoMock;
        private readonly Mock<IPasswordService> _encryptDecrypt;

        UserController _controller;

        public UserControllerTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _repoMock = new Mock<IUserRepository>();
            _encryptDecrypt = new Mock<IPasswordService>();
            _service = new UserService(mapper: _mapper, repo: _repoMock.Object, encryptDecrypt: _encryptDecrypt.Object);
            _controller = new UserController(_mapper, _service);
        }

        #region Create

        [Fact(DisplayName = "Create valid user")]
        [Trait("Category", "Controllers")]
        public void Create_WhenUserIsValid_ReturnsUserDTO()
        {
            // Arrange
            string encryptedPassword = "@A147d258g369";

            var create = UserFixture.CreateValidCreateUser(encryptedPassword);

            _encryptDecrypt.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptedPassword);

            // Act
            var okResult = _controller.Create(create);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact(DisplayName = "Create When User Exists")]
        [Trait("Category", "Controllers")]
        public void Create_WhenUserExists_ThrowsNewDomainException()
        {
            // Arrange
            var create = UserFixture.CreateValidUserDTO();
            var exists = UserFixture.CreateValidUser();

            _repoMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => exists);

            var userToCreateController = _mapper.Map<CreateUserRequest>(_mapper.Map<UserDTO>(create));

            // Act
            var okResult = _controller.Create(userToCreateController);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.Equal($"Já existe um usuário cadastrado com o email: {create.Email} informado.", result.Message);
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Create When User is Invalid")]
        [Trait("Category", "Controllers")]
        public void Create_WhenUserIsInvalid_ThrowsNewDomainException()
        {
            // Arrange
            var userToCreate = UserFixture.CreateInvalidUserDTO();

            var userToCreateController = _mapper.Map<CreateUserRequest>(_mapper.Map<UserDTO>(userToCreate));

            // Act
            var okResult = _controller.Create(userToCreateController);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.Equal("Alguns campos estão inválidos, por favor corrija-os!", result.Message);
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Create When User Nome is Invalid")]
        [Trait("Category", "Controllers")]
        public void Create_NomeInvalid_ThrowsNewRequest()
        {
            // Arrange
            var userToCreate = UserFixture.CreateInvalidCreateUserName();

            // Act
            var okResult = _controller.Create(userToCreate);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;
            // Assert
            Assert.Equal("Alguns campos estão inválidos, por favor corrija-os!", result.Message);
            Assert.Equal("O nome não pode ser vazio.", result.Data[0]);
            Assert.Equal("O nome deve ter no mínimo 3 caracteres.", result.Data[1]);
            Assert.False(result.Success);
        }

        

        [Fact(DisplayName = "Create When User Password Null is Invalid")]
        [Trait("Category", "Controllers")]
        public void Create_PasswordNullInvalid_ThrowsNewRequest()
        {
            // Arrange
            var userToCreate = UserFixture.CreateInvalidCreateUserPasswordNull();

            // Act
            var okResult = _controller.Create(userToCreate);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.Equal("Alguns campos estão inválidos, por favor corrija-os!", result.Message);
            Assert.Equal("A senha não pode ser vazia.", result.Data[0]);
            Assert.Equal("A senha deve ter no mínimo 6 caracteres.", result.Data[1]);
            Assert.Equal("Informe a senha", result.Data[2]);
            Assert.Equal("A senha deve ter no mínimo 8 caracteres.", result.Data[3]);
            Assert.Equal("Senha deve ter pelo menos 1 caracter minúsculo", result.Data[4]);
            Assert.Equal("Senha deve ter pelo menos 1 caracter maiúsculo", result.Data[5]);
            Assert.Equal("Digite pelo menos 1 caracter especial (p. ex): ~,!,@,#,$,%,^,&,*,(,),+,=,?", result.Data[6]);
            Assert.Equal("Não poder ter caracteres repetidos em sequência", result.Data[7]);
            Assert.Equal("Senha deve ter pelo menos 1 número", result.Data[8]);
            Assert.False(result.Success);
        }

        #endregion

        #region Update

        [Fact(DisplayName = "Update Valid User")]
        [Trait("Category", "Controllers")]
        public void Update_WhenUserIsValid_ReturnsUserDTO()
        {
            // Arrange
            var oldUser = UserFixture.CreateValidUser();
            var userToUpdate = UserFixture.CreateValidUserDTO();
            var userUpdated = _mapper.Map<User>(userToUpdate);

            _repoMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => oldUser);
            _repoMock.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(() => userUpdated);

            var userToUpdateController = _mapper.Map<UpdateUserRequest>(_mapper.Map<UserDTO>(userToUpdate));

            // Act
            var okResult = _controller.Update(userToUpdateController);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            // Assert
            Assert.Equal("Usuário atualizado com sucesso!", result.Message);
            Assert.True(result.Success);

        }

        [Fact(DisplayName = "Update When User Not Exists")]
        [Trait("Category", "Controllers")]
        public void Update_WhenUserNotExists_ThrowsNewDomainException()
        {
            // Arrange
            var update = UserFixture.CreateValidUserDTO();

            var userToCreateController = _mapper.Map<UpdateUserRequest>(_mapper.Map<UserDTO>(update));

            // Act
            var okResult = _controller.Update(userToCreateController);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.Equal($"Não existe nenhum usuário com o id: {update.Id} informado!", result.Message);
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Update When User is Invalid")]
        [Trait("Category", "Controllers")]
        public void Update_WhenUserIsInvalid_ThrowsNewDomainException()
        {
            // Arrange
            var userToUpdate = UserFixture.CreateInvalidUserDTO();

            var userToCreateController = _mapper.Map<UpdateUserRequest>(_mapper.Map<UserDTO>(userToUpdate));

            // Act
            var okResult = _controller.Update(userToCreateController);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.Equal($"Não existe nenhum usuário com o id: {userToUpdate.Id} informado!", result.Message);
            Assert.False(result.Success);
        }
        #endregion


        #region Remove

        [Fact(DisplayName = "Remove User")]
        [Trait("Category", "Controllers")]
        public void Remove_WhenUserExists_RemoveUser()
        {
            // Arrange
            var userId = new Randomizer().Int(0, 1000);

            // Act
            var okResult = _controller.Remove(userId);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuário removido com sucesso!", result.Message);
            Assert.True(result.Success);
        }

        #endregion

        #region Get

        [Fact(DisplayName = "Get By Id")]
        [Trait("Category", "Controllers")]
        public void GetById_WhenUserExists_ReturnsUserDTO()
        {
            // Arrange
            var userId = new Randomizer().Int(0, 1000);
            var userFound = UserFixture.CreateValidUser();

            _repoMock.Setup(x => x.Get(userId)).ReturnsAsync(() => userFound);

            // Act
            var okResult = _controller.Get(userId);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuário encontrado com sucesso!", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Get By Id When User Not Exists")]
        [Trait("Category", "Controllers")]
        public void GetById_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var userId = new Randomizer().Int(0, 1000);

            _repoMock.Setup(x => x.Get(userId)).ReturnsAsync(() => null);

            // Act
            var okResult = _controller.Get(userId);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Nenhum usuário foi encontrado com o ID informado.", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Get By Email")]
        [Trait("Category", "Controllers")]
        public void GetByEmail_WhenUserExists_ReturnsUserDTO()
        {
            // Arrange
            var userEmail = new Internet().Email();
            var userFound = UserFixture.CreateValidUser();

            _repoMock.Setup(x => x.GetByEmail(userEmail)).ReturnsAsync(() => userFound);

            // Act
            var okResult = _controller.GetByEmail(userEmail);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuário encontrado com sucesso!", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Get By Email When User Not Exists")]
        [Trait("Category", "Controllers")]
        public void GetByEmail_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var userEmail = new Internet().Email();

            _repoMock.Setup(x => x.GetByEmail(userEmail)).ReturnsAsync(() => null);

            // Act
            var okResult = _controller.GetByEmail(userEmail);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Nenhum usuário foi encontrado com o email informado.", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Get All Users")]
        [Trait("Category", "Controllers")]
        public void GetAllUsers_WhenUsersExists_ReturnsAListOfUserDTO()
        {
            // Act
            var okResult = _controller.Get();
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuários encontrados com sucesso!", result.Message);
            Assert.True(result.Success);
        }
        #endregion

        #region Search

        [Fact(DisplayName = "Search By Name")]
        [Trait("Category", "Controllers")]
        public void SearchByName_WhenAnyUserFound_ReturnsAListOfUserDTO()
        {
            // Arrange
            var nameToSearch = new Name().FirstName();
            var usersFound = UserFixture.CreateListValidUser();

            _repoMock.Setup(x => x.SearchByName(nameToSearch)).ReturnsAsync(() => usersFound);

            // Act
            var okResult = _controller.SearchByName(nameToSearch);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuário encontrado com sucesso!", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Search By Name When None User Found")]
        [Trait("Category", "Controllers")]
        public void SearchByName_WhenNoneUserFound_ReturnsEmptyList()
        {
            // Arrange
            var nameToSearch = new Name().FirstName();

            _repoMock.Setup(x => x.SearchByName(nameToSearch)).ReturnsAsync(() => null);

            // Act
            var okResult = _controller.SearchByName(nameToSearch);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Nenhum usuário foi encontrado com o nome informado", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Search By Email")]
        [Trait("Category", "Controllers")]
        public void SearchByEmail_WhenAnyUserFound_ReturnsAListOfUserDTO()
        {
            // Arrange
            var emailSoSearch = new Internet().Email();
            var usersFound = UserFixture.CreateListValidUser();

            _repoMock.Setup(x => x.SearchByEmail(emailSoSearch)).ReturnsAsync(() => usersFound);

            // Act
            var okResult = _controller.SearchByEmail(emailSoSearch);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Usuário encontrado com sucesso!", result.Message);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Search By Email When None User Found")]
        [Trait("Category", "Controllers")]
        public void SearchByEmail_WhenNoneUserFound_ReturnsEmptyList()
        {
            // Arrange
            var emailSoSearch = new Internet().Email();

            _repoMock.Setup(x => x.SearchByEmail(emailSoSearch)).ReturnsAsync(() => null);

            // Act
            var okResult = _controller.SearchByEmail(emailSoSearch);
            var item = okResult.Result as ObjectResult;
            var result = item.Value as ResponseDefault;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Nenhum usuário foi encontrado com o email informado", result.Message);
            Assert.True(result.Success);
        }

        #endregion

    }
}
