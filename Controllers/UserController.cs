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

        public UserController(IUserRepositorie repositorie)
        {
            _repositorie = repositorie;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User userModel)
        {
            User user = await _repositorie.Create(userModel);
            if(await _repositorie.DataIsUnique(user))
            {
                return Ok(user);

            }
            return BadRequest("CPF/CNPJ já está cadastro");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromBody] User userModel, Guid id)
        {
            userModel.Id = id;  
            User user = await _repositorie.Update(userModel, id);
            if (await _repositorie.DataIsUnique(user))
            {
                return Ok(user);

            }
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
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(Guid id)
        {
            bool deleted = await _repositorie.Delete(id);
            return Ok(deleted);
    }
}
}
