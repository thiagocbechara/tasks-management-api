using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Chunker
    {
        public static IEnumerable<string> ChunkIter(string s, int chunks)
        {
            var count = s.Length / chunks;
            var remain = s.Length - count * chunks;
            var index = 0;
            while (index < s.Length)
            {
                var length = count;
                if (remain > 0)
                {
                    length += 1;
                    remain--;
                }
                var stringChunk = s.Substring(index, length);
                yield return stringChunk;
                index += length;
            }
        }

        public static string[] Chunk(string s, int chunks)
        {
            if (chunks <= 0)
                throw new ArgumentException(nameof(chunks));
            if (string.IsNullOrWhiteSpace(s))
                return new string[0];

            chunks = chunks > s.Length ? s.Length : chunks;

            return ChunkIter(s, chunks).ToArray();
        }
    }
}
