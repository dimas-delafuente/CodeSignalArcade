using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace CodeSignalArcade
{
    class Program
    {
        private static Dictionary<string, Type> Challenges = new Dictionary<string, Type>
        {
            {"1", typeof(AddBorder) },
            {"2", typeof(AdjacentElementsProduct) },
            {"3", typeof(AlternatingSums) },
            {"4", typeof(ArrayConsecutiveStatues) },
            {"5", typeof(CommonCharacterCount) },
            {"6", typeof(IsLuckyNumber) },
            {"7", typeof(Palindrome) },
            {"8", typeof(ReverseInParentheses) },
            {"9", typeof(SortByHeight) },
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Write Code Signal Arcade challenge number to run benchmark:");
            PrintChallenges();

            Console.WriteLine("Challenge: ");
            string selectedChallenge = Console.ReadLine();
            if (string.IsNullOrEmpty(selectedChallenge) || !Challenges.ContainsKey(selectedChallenge))
            {
                Console.WriteLine("Invalid challenge selected. Please select a valid challenge.");
            } else
            {
                _ = BenchmarkRunner.Run(Challenges[selectedChallenge]);
            }

        }

        private static void PrintChallenges()
        {
            Console.WriteLine();
            
            foreach(var challenge in Challenges)
            {
                Console.WriteLine($"{challenge.Key} = {challenge.Value.Name}");
            }

            Console.WriteLine();
        }
    }
}
