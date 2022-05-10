using Demo.Domain.Entities;
using Demo.Domain.Validators;
using Xunit;

namespace Demo.Tests.Projects.DemoDomain
{
    public class UserTest
    {
        [Fact]
        public void User_IsValide()
        {
            // Arrange
            var user = new User(
                name: "Teste Nome",
                email: "teste@teste.com",
                password: "@A147d258g369");

            var validator = new UserValidator();

            Assert.Equal("Teste Nome", user.Name);
            Assert.Equal("teste@teste.com", user.Email);
            Assert.Equal("@A147d258g369", user.Password);
            Assert.True(user.Validate());
            Assert.True(validator.Validate(user).IsValid);

            Assert.NotNull(user.Name);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Password);

            Assert.NotEmpty(user.Name);
            Assert.NotEmpty(user.Email);
            Assert.NotEmpty(user.Password);
        }

    }
}
