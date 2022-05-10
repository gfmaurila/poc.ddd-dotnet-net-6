using Demo.Application.Demo.DTO.DTO;
using Xunit;

namespace Demo.Tests.Projects.DemoApplicationDemoDTO
{
    public class UserDTOTest
    {
        [Fact]
        public void User_IsValide()
        {
            // Arrange
            var user = new UserDTO(
                id: 0,
                name: "Teste Nome",
                email: "teste@teste.com",
                password: "@A147d258g369");

            Assert.Equal("Teste Nome", user.Name);
            Assert.Equal("teste@teste.com", user.Email);
            Assert.Equal("@A147d258g369", user.Password);
            
            Assert.NotNull(user.Name);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Password);

            Assert.NotEmpty(user.Name);
            Assert.NotEmpty(user.Email);
            Assert.NotEmpty(user.Password);
        }

    }
}
