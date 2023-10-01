using AutoMapper;
using eventz.DTOs;
using eventz.Mappings;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eventz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorie _repositorie;
        private readonly IMapper _mapper;

        

        public UserController(IUserRepositorie repositorie, IMapper mapper)
        {
            _repositorie = repositorie;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> Create([FromBody] User userModel)
        {
            if(await _repositorie.DataIsUnique(userModel))
            {
                userModel.Id = Guid.NewGuid();
                User user = await _repositorie.Create(userModel);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            else
                return BadRequest("CPF/CNPJ já está cadastro");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User user)
        {
            var userLoggin = await _repositorie.AuthenticateAsync(user.Username, user.Password);
            if (userLoggin == false )
            {
                return NotFound("Usuario ou senha inválidos!");
            }
            var token = _repositorie.GenerateToken(user.Id, user.Email);

            return token;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromBody] User userModel, Guid id)
        {
            userModel.Id = id;  
            if (await _repositorie.DataIsUnique(userModel))
            {
                User user = await _repositorie.Update(userModel, id);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            else
                return BadRequest("CPF/CNPJ já está cadastro");

        }

        [HttpGet]
        public async Task <ActionResult<List<User>>> GetAllUsers()
        {
            List<User> users = await _repositorie.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUserById(Guid id)
        {
            User user = await _repositorie.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(Guid id)
        {
            bool deleted = await _repositorie.Delete(id);
            return Ok(deleted);
    }
}
}
