using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace CodeSignalArcade
{
    public class CommonCharacterCount
    {
        [Params("aabcc", "zzzz")]
        public string InputString1;

        [Params("adcaa", "zzzzzzz")]
        public string InputString2;

        [Benchmark]
        public int CommonCharacterCountLookUp()
        {
            ILookup<char, char> s1chars = InputString1.ToLookup(x => x);
            ILookup<char, char> s2chars = InputString2.ToLookup(x => x);
            ILookup<char, char> longest;
            ILookup<char, char> smallest;

            int s1CharCount = s1chars.Count();
            int s2CharCount = s1chars.Count();

            if (s1CharCount > s2CharCount)
            {
                longest = s1chars;
                smallest = s2chars;
            }
            else
            {
                longest = s2chars;
                smallest = s1chars;
            }

            int total = 0;
            foreach (var charGroup in smallest)
            {
                if (longest.Contains(charGroup.Key))
                {
                    int s1Count = charGroup.Count();
                    int s2Count = longest[charGroup.Key].Count();
                    int compare = s1Count.CompareTo(s2Count);

                    int maxCharCount = compare == 0 ? s1Count : (s1Count > s2Count ? s2Count : s1Count);
                    total += maxCharCount;
                }
            }

            return total;
        }

        [Benchmark]
        public int CommonCharacterCountDirect()
        {
            return InputString1.Distinct().Sum(_ => Math.Min(InputString1.Count(l => l == _), InputString2.Count(l => l == _)));
        }

        [Benchmark]
        public int CommonCharacterCountRemove()
        {
            int count = 0;
            foreach (char c in InputString1)
            {
                int index = InputString2.IndexOf(c);
                if (index >= 0)
                {
                    InputString2 = InputString2.Remove(index, 1);
                    count++;
                }
            }

            return count;
        }

        [Benchmark]
        public int CommonCharacterCountGroupBy()
        {
            return InputString1.GroupBy(c => c)
                    .Join(
                        InputString2.GroupBy(c => c),
                        g => g.Key,
                        g => g.Key,
                        (lg, rg) => lg.Zip(rg, (l, r) => l).Count())
                    .Sum();
        }

    }
}
