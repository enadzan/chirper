using System;
using System.Linq;
using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class HashTagRepository: Repository<HashTag>, IHashTagRepository
    {
        public HashTagRepository(ChirpDbContext context) : base(context)
        {
        }

        public HashTag Find(string tag, DateTime timeUtc, long chirpId)
        {
            return Context.Set<HashTag>()
                .Find(tag, timeUtc, chirpId);
        }

        public bool Exists(string tag, DateTime timeUtc, long chirpId)
        {
            return Context.Set<HashTag>()
                .Any(h => h.Tag == tag && h.TimeUtc == timeUtc && h.ChirpId == chirpId);
        }
    }
}
