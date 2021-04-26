using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace CodeSignalArcade
{
    public class IsLuckyNumber
    {
        [Params(1230, 999999, 11, 134008)]
        public int Number;

        [Benchmark]
        public bool IsLuckyInHalfs()
        {

            string number = Number.ToString();
            string firstHalf = number.Substring(0, (number.Length / 2));
            string secondHalf = number.Substring(number.Length / 2, (number.Length / 2));

            int sumFirstHalf = 0;
            int sumSecondHalf = 0;
            for (int i = 0; i < firstHalf.Length; i++)
            {
                sumFirstHalf += Int32.Parse(firstHalf[i].ToString());
                sumSecondHalf += Int32.Parse(secondHalf[i].ToString());
            }

            return sumFirstHalf == sumSecondHalf;
        }


        [Benchmark]
        public bool IsLuckySumRemove()
        {
            var numberString = Number.ToString();
            int half = numberString.Length / 2;
            return numberString.Substring(half).Sum(_ => _ - '0') == numberString.Remove(half).Sum(_ => _ - '0');
        }

        [Benchmark]
        public bool IsLuckyPow()
        {
            int length = 1 + (int)Math.Log(Number, 10);
            int div = (int)Math.Pow(10, length / 2);
            int left = Number / div;
            int right = Number % div;

            int sumL = 0;
            int sumR = 0;

            while (left > 0)
            {
                sumL += left % 10;
                sumR += right % 10;
                left /= 10;
                right /= 10;
            }

            return sumL == sumR;
        }
    }
}
