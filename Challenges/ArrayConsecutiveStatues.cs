using BenchmarkDotNet.Attributes;
using System.Linq;

namespace CodeSignalArcade
{
    public class ArrayConsecutiveStatues
    {
        [Params(new int[] { 6, 2, 3, 8 }, new int[] { 0, 3 }, new int[] { 5, 4, 6 })]
        public int[] InputArray;

        [Benchmark]
        public int MakeArrayConsecutive()
        {

            var st = InputArray.OrderBy(x => x);
            int total = 0;

            for (int i = 0; i < InputArray.Length - 1; i++)
            {
                total += st.ElementAt(i + 1) - st.ElementAt(i) - 1;
            }

            return total;
        }

        [Benchmark]
        public int MakeArrayConsecutiveMinMax()
        {
            return InputArray.Max() - InputArray.Min() - InputArray.Length + 1;
        }
    }
}
