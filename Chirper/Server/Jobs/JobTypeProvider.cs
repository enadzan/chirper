using System;
using MassiveJobs.Core;

namespace Chirper.Server.Jobs
{
    public class JobTypeProvider: IJobTypeProvider
    {
        private const string TagTimelineUpdate = "tu";
        private const string TagTimelineUpdateArgs = "tua";
        private const string TagFollowerTimelineUpdate = "fu";
        private const string TagFollowerTimelineUpdateArgs = "fua";

        public Type TagToType(string tag)
        {
            switch (tag)
            {
                case TagTimelineUpdate: return typeof(TimelineUpdate);
                case TagTimelineUpdateArgs: return typeof(TimelineUpdateArgs);

                case TagFollowerTimelineUpdate: return typeof(FollowerTimelineUpdate);
                case TagFollowerTimelineUpdateArgs: return typeof(FollowerTimelineUpdateArgs);

                default: throw new Exception("unknown tag: " + tag);
            }
        }

        public string TypeToTag(Type type)
        {
            if (type == typeof(TimelineUpdate)) return TagTimelineUpdate;
            if (type == typeof(TimelineUpdateArgs)) return TagTimelineUpdateArgs;

            if (type == typeof(FollowerTimelineUpdate)) return TagFollowerTimelineUpdate;
            if (type == typeof(FollowerTimelineUpdateArgs)) return TagFollowerTimelineUpdateArgs;

            throw new Exception("unsupported type: " + type.FullName);
        }
    }
}
