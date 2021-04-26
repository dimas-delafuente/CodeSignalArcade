using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace CodeSignalArcade
{
    public class Palindrome
    {
        [Params("aba", "ababababababa", "hlbeeykoqqqokyeeblh")]
        public string InputString;

        [Benchmark]
        public bool CheckPalindromeBinary()
        {
            int left = 0;
            int right = InputString.Length - 1;

            while (right > left)
            {
                if (InputString[left] != InputString[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        [Benchmark]
        public bool CheckPalindromeReverseSequence()
        {
            return InputString.SequenceEqual(InputString.Reverse());
        }

        [Benchmark]
        public bool CheckPalindromeReverse()
        {
            return InputString.Reverse().Equals(InputString);
        }
    }
}
