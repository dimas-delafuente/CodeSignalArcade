using BenchmarkDotNet.Attributes;
using System.Linq;

namespace CodeSignalArcade
{
    public class AddBorder
    {
        [Params(new string[] { "abc", "ded" }, new string[] { "abcde", "fghij", "klmno", "pqrst", "uvwxy" })]
        public string[] Picture;

        [Benchmark]
        public string[] AddBorderFor()
        {
            string[] borderedString = new string[Picture.Length + 2];

            for (int i = 0; i < Picture.Length; i++)
            {
                borderedString[i + 1] = $"*{Picture[i]}*";
            }

            borderedString[0] = borderedString[borderedString.Length - 1] = new string('*', (Picture[0].Length + 2));

            return borderedString;
        }

        [Benchmark]
        public string[] AddBorderConcat()
        {
            var stars = new string[] { new string('*', Picture[0].Length + 2) };
            return stars.Concat(Picture.Select(s => "*" + s + "*"))
                .Concat(stars)
                .ToArray();

        }

    }
}
