using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace CodeSignalArcade
{
    public class AdjacentElementsProduct
    {
        [Params(new int[] { 3, 6, -2, -5, 7, 3 }, new int[] { 9, 5, 10, 2, 24, -1, -48, 1, 2, 3, 0 }, new int[] { 3, 6, -2, -5, 7, 3, 9, 5, 10, 2, 24, -1, -48, 1, 2, 3, 0 })]
        public int[] InputArray;

        [Benchmark]
        public int AdjacentElementsProductBinary()
        {

            int left = 0;
            int right = InputArray.Length - 1;
            int highest = Int32.MinValue;

            while (right > left)
            {
                int productLeft = InputArray[left] * InputArray[left + 1];
                int productRight = InputArray[right] * InputArray[right - 1];

                if (productLeft > highest)
                    highest = productLeft;

                if (productRight > highest)
                    highest = productRight;

                left++;
                right--;
            }

            return highest;

        }

        [Benchmark]
        public int AdjacentElementsProductSequence()
        {
            int prod = Int32.MinValue;
            for (int i = 1; i < InputArray.Length; i++)
            {
                if (InputArray[i] * InputArray[i - 1] > prod)
                {
                    prod = InputArray[i] * InputArray[i - 1];
                }
            }
            return prod;
        }

    }
}
