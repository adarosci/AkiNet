using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkiNet
{
    public static class Extensions
    {
        public static Dictionary<TKey, TValue> AddEx<TKey, TValue>(this Dictionary<TKey, TValue> ob, TKey key, TValue value)
        {
            ob.Add(key, value);
            return ob;
        }

        public static Dictionary<TKey, TValue> AddRangeEx<TKey, TValue>(this Dictionary<TKey, TValue> ob, IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            if (keys.Count() != values.Count())
                throw new IndexOutOfRangeException("Keys count and values count should be same!");
            for (int i = 0; i < keys.Count(); i++)
            {
                ob.Add(keys.Skip(i).First(), values.Skip(i).First());
            }
            return ob;
        }
    }
}
