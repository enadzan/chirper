using System;
using Microsoft.AspNetCore.Mvc;

using Chirper.Shared;
using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;
using Chirper.Server.Jobs;

namespace Chirper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpDb _db;

        public ChirpsController(IChirpDb db)
        {
            _db = db;
        }

        public IActionResult Post(DtoChirpPost dto)
        {
            _db.BeginTransaction();

            var chirp = new Chirp
            {
                UserId = int.Parse(User.Identity.Name ?? "1"),
                ChirpTimeUtc = DateTime.UtcNow,
                ChirpType = ChirpType.Chirp,
                Contents = dto.Contents
            };

            _db.Chirps.Add(chirp);

            _db.SaveChanges();

            TimelineUpdate.Publish(new TimelineUpdateArgs
            {
                ChirpId = chirp.Id,
                AuthorId = chirp.UserId
            }, 60 * 1000); // this job may take longer if a user has a lot of followers, we allow one minute

            _db.CommitTransaction();

            return Ok(chirp.Id);
        }
    }
}
