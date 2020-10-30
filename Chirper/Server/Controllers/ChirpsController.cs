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

            // Publish chirp processing job, before commit.
            // If the publishing fails, the transaction will be rolled back.
            //
            // We're using the job publish as the last committing resource because
            // it does not participate in the DB transaction.

            ChirpProcessing.Publish(chirp.Id);

            _db.CommitTransaction();

            return Ok(chirp.Id);
        }
    }
}
