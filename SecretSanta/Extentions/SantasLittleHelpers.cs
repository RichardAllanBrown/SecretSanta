using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Extentions
{
    public static class SantasLittleHelpers
    {
        public static IList<T> GetShuffle<T>(this IList<T> source)
        {
            var rand = new Random();
            return source.OrderBy(x => rand.Next()).ToList();
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(this IList<T> source)
        {
            return source.GetPermutations(source.Count);
        }

        private static IEnumerable<IList<T>> GetPermutations<T>(this IList<T> source, int count)
        {
            if (count <= 1)
            {
                yield return source;
                yield break;
            }

            for (int i = 0; i < count; i++)
            {
                foreach(var subPerm in source.GetPermutations(count - 1))
                {
                    yield return subPerm;
                }

                source.RotateRight();
            }
        }

        private static void RotateRight<T>(this IList<T> source)
        {
            var rotateBuffer = source[source.Count - 1];
            source.RemoveAt(source.Count - 1);
            source.Insert(0, rotateBuffer);
        }

        public static IDictionary<K,V> ToDictionary<K,V>(this IEnumerable<KeyValuePair<K,V>> source)
        {
            var dict = new Dictionary<K,V>();
            foreach (var pair in source)
                dict[pair.Key] = pair.Value;

            return dict;
        }

        public static IEnumerable<KeyValuePair<K,V>> ZipToKV<K,V>(this IEnumerable<K> left, IEnumerable<V> right)
        {
            return left.Zip(right, (l, r) => new KeyValuePair<K,V>(l, r));
        }
    }
}
