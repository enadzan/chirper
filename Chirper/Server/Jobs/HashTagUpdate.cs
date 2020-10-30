using System;
using System.Linq;
using System.Text.RegularExpressions;

using MassiveJobs.Core;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.Jobs
{
    public class HashTagUpdate: Job<HashTagUpdate, long>
    {
        private static readonly Regex HashTagRegex
            = new Regex(@"\B(\#[a-zA-Z]+[0-9]*\b)(?!;)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly IChirpDb _db;

        public HashTagUpdate(IChirpDb db)
        {
            _db = db;
        }

        public override void Perform(long chirpId)
        {
            var chirp = _db.Chirps.Find(chirpId);
            if (chirp == null) throw new Exception($"Chirp {chirpId} doesn't exist");

            var hashTags = HashTagRegex.Matches(chirp.Contents).Select(m => m.Value).ToList();
            if (hashTags.Count == 0) return;

            if (_db.HashTags.Exists(hashTags[0].Substring(1), chirp.ChirpTimeUtc, chirp.Id)) return; // idempotent

            foreach (var tag in hashTags)
            {
                _db.HashTags.Add(new HashTag
                {
                    Tag = tag.Substring(1),
                    TimeUtc = chirp.ChirpTimeUtc,
                    ChirpId = chirp.Id
                });
            }

            _db.SaveChanges();
        }
    }
}
