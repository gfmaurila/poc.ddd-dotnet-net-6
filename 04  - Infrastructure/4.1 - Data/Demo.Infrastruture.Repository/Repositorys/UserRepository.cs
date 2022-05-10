using Demo.Domain.Core.Interfaces.Repositorys;
using Demo.Domain.Entities;
using Demo.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastruture.Repository.Repositorys
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EFContext _context;

        public UserRepository(EFContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users.Where(x =>x.Email.ToLower() == email.ToLower()).AsNoTracking().ToListAsync();
            return user.FirstOrDefault();
        }

        public async Task<List<User>> SearchByEmail(string email)
        {
            var allUsers = await _context.Users.Where(x => x.Email.ToLower().Contains(email.ToLower())).AsNoTracking().ToListAsync();
            return allUsers;
        }

        public async Task<List<User>> SearchByName(string name)
        {
            var allUsers = await _context.Users.Where(x => x.Name.ToLower().Contains(name.ToLower())).AsNoTracking().ToListAsync();
            return allUsers;
        }
    }
}
