using Demo.Domain.Entities;

namespace Demo.Domain.Core.Interfaces.Repositorys
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<List<User>> SearchByEmail(string email);
        Task<List<User>> SearchByName(string name);
    }
}
