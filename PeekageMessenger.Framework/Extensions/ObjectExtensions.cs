using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PeekageMessenger.Framework
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
