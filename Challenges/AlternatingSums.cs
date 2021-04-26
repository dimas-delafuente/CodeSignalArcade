using BenchmarkDotNet.Attributes;
using System.Linq;

namespace CodeSignalArcade
{
    public class AlternatingSums
    {
        [Params(new int[] { 50, 60, 60, 45, 70 }, new int[] { 50, 60, 60, 45, 70, 100, 51, 50, 100 })]
        public int[] People;

        [Benchmark]
        public int[] AlternatingSumsModule()
        {
            int[] weights = new int[2] { 0, 0 };

            for (int i = 0; i < People.Length; i++)
            {
                weights[i % 2] += People[i];
            }

            return weights;
        }

        [Benchmark]
        public int[] AlternatingSumsSelect()
        {
            var sums = new int[2];
            People.Select((_, i) => sums[i % 2] += _);
            return sums;
        }

        [Benchmark]
        public int[] AlternatingSumsWhere()
        {
            var odd = People.Where((c, i) => i % 2 != 0);
            var even = People.Where((c, i) => i % 2 == 0);
            return new int[] { even.Sum(), odd.Sum() };
        }
    }
}
