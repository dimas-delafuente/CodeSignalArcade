using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeSignalArcade
{
    public class ReverseInParentheses
    {
        [Params("(bar)", "foo(bar)baz", "foo(bar)baz(blim)")]
        public string InputString;

        //[Benchmark]
        public string ReverseInParenthesesRecursiveBenchmark()
        {
            return this.ReverseInParenthesesRecursive(InputString);
        }

        private string ReverseInParenthesesRecursive(string inputString)
        {
            int leftParenthesisIndex = inputString.IndexOf("(");

            if (leftParenthesisIndex >= 0)
            {

                string leftText = inputString.Substring(0, leftParenthesisIndex);
                string outputString = inputString.Substring(leftParenthesisIndex + 1);

                if (outputString.Contains("("))
                    outputString = ReverseInParenthesesRecursive(outputString);

                string innerText = new string(outputString.Substring(0, outputString.IndexOf(")")).Reverse().ToArray());
                string remainingText = outputString.IndexOf(")") >= 0 ? outputString.Substring(outputString.IndexOf(")") + 1) : string.Empty;

                outputString = $"{leftText}{innerText}{remainingText}";

                return outputString;
            }

            return InputString;
        }

        [Benchmark]
        public string ReverseInParenthesesWhileLoop()
        {
            while (InputString.Contains("("))
            {
                int innerParentheses = InputString.LastIndexOf("(");
                string innerText = new string(InputString.Skip(innerParentheses + 1).TakeWhile(x => x != ')').ToArray());
                string textToRemove = $"({innerText})";
                string reversedInnerText = new string(innerText.Reverse().ToArray());
                InputString = InputString.Replace(textToRemove, reversedInnerText);
            }
            return InputString;
        }

        //[Benchmark]
        public string ReverseInParenthesesRegex()
        {
            while (InputString.Contains("("))
            {
                InputString = Regex.Replace(InputString, @"\(\w*\)", match => {
                    return String.Concat(match.Value.Trim(new[] { '(', ')' }).Reverse());
                });
            }
            return InputString;
        }


    }
}
