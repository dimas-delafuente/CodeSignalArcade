using BenchmarkDotNet.Attributes;
using System.Linq;

namespace CodeSignalArcade
{
    public class SortByHeight
    {
        [Params(new int[]{-1, 150, 190, 170, -1, -1, 160, 180}, 
            new int[] { 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1 }, 
            new int[] { 2, -1, -1, -1, -1, -1, -1, -1, 150, 190, 170, -1, -1, 160, 180, -1, -1, -1, -1, -1, -1, 1 })]
        public int[] Row;

        [Benchmark]
        public int[] SortByHeightWithList()
        {
            var orderedItems = Row.Where(item => item != -1).OrderBy(item => item).ToArray();

            int peopleCount = 0;
            for (int i = 0; i < Row.Length && peopleCount < orderedItems.Length; i++)
            {
                if (Row[i] == -1)
                    continue;

                Row[i] = orderedItems[peopleCount++];
            }

            return Row;
        }

        [Benchmark]
        public object SortByHeightWithSelect()
        {
            int[] people = Row.Where(p => p >= 0).OrderBy(p => p).ToArray();
            int i = 0;

            return Row.Select(p => p >= 0 ? people[i++] : -1);
        }

        [Benchmark]
        public int[] SortByHeightForEach()
        {
            int i = 0;
            foreach (var h in Row.Where(_ => _ != -1).OrderBy(_ => _))
            {
                for (; Row[i] == -1; i++);
                Row[i++] = h;
            }
            return Row;
        }




    }
}
