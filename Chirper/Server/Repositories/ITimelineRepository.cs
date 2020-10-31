using System;
using System.Collections.Generic;

using Chirper.Server.DomainModel;

namespace Chirper.Server.Repositories
{
    public interface ITimelineRepository: IRepository<Timeline>
    {
        Timeline Find(int userId, DateTime timeUtc, long chirpId);
        List<Timeline> FindByTime(int userId, DateTime fromUtc, DateTime toUtc, long? afterChirpId, int batchSize);
    }
}
