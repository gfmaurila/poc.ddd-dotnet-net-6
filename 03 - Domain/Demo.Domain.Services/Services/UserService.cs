using AutoMapper;
using Demo.Application.Demo.DTO.DTO;
using Demo.Domain.Core.Interfaces.Repositorys;
using Demo.Domain.Core.Interfaces.Services;
using Demo.Domain.Entities;
using Demo.Infrastruture.CrossCutting.Exceptions;
using PocNugetEncryptDecrypt.Interfaces;

namespace Demo.Domain.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;
        private readonly IPasswordService _encryptDecrypt;

        public UserService(IMapper mapper, IUserRepository repo, IPasswordService encryptDecrypt)
        {
            _mapper = mapper;
            _repo = repo;
            _encryptDecrypt = encryptDecrypt;
        }

        public async Task<UserDTO> Create(UserDTO dto)
        {
            var isUser = await _repo.GetByEmail(dto.Email);

            if (isUser != null)
                throw new Exceptions($"Já existe um usuário cadastrado com o email: {dto.Email} informado.");

            var user = _mapper.Map<User>(dto);
            user.Validate();
            user.ChangePassword(_encryptDecrypt.Encrypt(user.Password));

            //user.ChangePassword(user.Password);
            var userCreated = await _repo.Create(user);
            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Update(UserDTO dto)
        {
            var exists = await _repo.Get(dto.Id);

            if (exists == null)
                throw new Exceptions($"Não existe nenhum usuário com o id: {dto.Id} informado!");

            var user = _mapper.Map<User>(dto);
            user.Validate();
            // user.ChangePassword(_encryptDecrypt.Encrypt(user.Password));
            // user.ChangePassword(_encryptDecrypt.Encrypt(user.Password));

            var updated = await _repo.Update(user);

            return _mapper.Map<UserDTO>(updated);
        }
        public async Task Remove(long id)
        {
            await _repo.Remove(id);
        }

        public async Task<UserDTO> Get(long id)
        {
            var user = await _repo.Get(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> Get()
        {
            var allUsers = await _repo.Get();
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var allUsers = await _repo.SearchByName(name);
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _repo.SearchByEmail(email);
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _repo.GetByEmail(email);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
