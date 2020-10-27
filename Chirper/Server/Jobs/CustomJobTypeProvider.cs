using System;
using MassiveJobs.Core;

namespace Chirper.Server.Jobs
{
    public class CustomJobTypeProvider: IJobTypeProvider
    {
        private const string TagTimelineUpdate = "tu";
        private const string TagTimelineUpdateArgs = "tua";
        private const string TagTimelineSingleUpdate = "ts";
        private const string TagTimelineSingleUpdateArgs = "tsa";

        public Type TagToType(string tag)
        {
            switch (tag)
            {
                case TagTimelineUpdate: return typeof(TimelineUpdate);
                case TagTimelineUpdateArgs: return typeof(TimelineUpdateArgs);

                case TagTimelineSingleUpdate: return typeof(TimelineSingleUpdate);
                case TagTimelineSingleUpdateArgs: return typeof(TimelineSingleUpdateArgs);

                default: throw new Exception("unknown tag: " + tag);
            }
        }

        public string TypeToTag(Type type)
        {
            if (type == typeof(TimelineUpdate)) return TagTimelineUpdate;
            if (type == typeof(TimelineUpdateArgs)) return TagTimelineUpdateArgs;

            if (type == typeof(TimelineSingleUpdate)) return TagTimelineSingleUpdate;
            if (type == typeof(TimelineSingleUpdateArgs)) return TagTimelineSingleUpdateArgs;

            throw new Exception("unsupported type: " + type.FullName);
        }
    }
}
