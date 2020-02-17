using System.Collections.Generic;

namespace PeekageMessenger.Framework.Core.Extensions
{
    public static class ObjectExtensions
    {
        static Dictionary<object, int> dictionary = new Dictionary<object, int>();
        static int objectId = 0;

        public static int GetId(this object obj)
        {
            if (dictionary.ContainsKey(obj))
                return dictionary[obj];

            return dictionary[obj] = ++objectId;
        }
    }
}
