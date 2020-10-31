using Microsoft.AspNetCore.Mvc;

using Chirper.Server.DomainModel;
using Chirper.Server.Infrastructure;
using Chirper.Server.Repositories;
using Chirper.Shared;

namespace Chirper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IChirpDb _db;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(IChirpDb db, IPasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public IActionResult Post(DtoUserRegister dto)
        {
            if (_db.Users.UserExists(dto.Username))
                ModelState.AddModelError("Username", "This username is already taken");

            if (dto.Password != dto.PasswordConfirm)
                ModelState.AddModelError("PasswordConfirm", "The password and the confirmation password do not match");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ChirpUser
            {
                Username = dto.Username,
                Password = _passwordHasher.HashPassword(dto.Password)
            };

            _db.Users.Add(user);

            _db.SaveChanges();

            return Ok(user.Id);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(DtoUserLogin dto)
        {
            var user = _db.Users.FindByUsername(dto.Username);

            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.Password))
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return BadRequest(ModelState);
            }

            return Ok(user.Id);
        }
    }
}
