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
    }
}
