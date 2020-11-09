using System;
using System.Collections.Generic;
using MassiveJobs.Core;

namespace Chirper.Server.Jobs
{
    public class CustomJobTypeProvider: IJobTypeProvider
    {
        private readonly Dictionary<string, Type> _tagToTypeMap = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> _typeToTagMap = new Dictionary<Type, string>();

        public CustomJobTypeProvider()
        {
            _tagToTypeMap["hu"] = typeof(HashTagUpdate);

            _tagToTypeMap["tu"] = typeof(TimelineUpdate);
            _tagToTypeMap["tua"] = typeof(TimelineUpdateArgs);

            _tagToTypeMap["tp"] = typeof(TestPeriodicJob);

            _tagToTypeMap["l"] = typeof(long);
            _tagToTypeMap["i"] = typeof(int);
            _tagToTypeMap["s"] = typeof(string);
            _tagToTypeMap["v"] = typeof(VoidArgs);

            foreach (var kvp in _tagToTypeMap)
            {
                _typeToTagMap[kvp.Value] = kvp.Key;
            }
        }

        public Type TagToType(string tag)
        {
            if (_tagToTypeMap.TryGetValue(tag, out var type)) return type;
            throw new Exception("unknown tag: " + tag);
        }

        public string TypeToTag(Type type)
        {
            if (_typeToTagMap.TryGetValue(type, out var tag)) return tag;
            throw new Exception("unsupported type: " + type.FullName);
        }
    }
}
