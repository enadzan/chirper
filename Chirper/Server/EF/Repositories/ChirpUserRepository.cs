﻿using System.Linq;
using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.EF.Repositories
{
    public class ChirpUserRepository: Repository<ChirpUser, int>, IChirpUserRepository
    {
        public ChirpUserRepository(ChirpDbContext context) : base(context)
        {
        }

        public bool UserExists(string username)
        {
            return Context.Set<ChirpUser>()
                .Any(u => u.Username == username);
        }

        public ChirpUser FindByUsername(string username)
        {
            return Context.Set<ChirpUser>()
                .FirstOrDefault(u => u.Username == username);
        }
    }
}
