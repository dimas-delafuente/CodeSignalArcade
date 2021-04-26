using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace CodeSignalArcade
{
    class Program
    {
        private static Dictionary<string, Type> Challenges = new Dictionary<string, Type>
        {
            {"3", typeof(Palindrome) },
            {"4", typeof(AdjacentElementsProduct) },
            {"6", typeof(ArrayConsecutiveStatues) },
            {"10", typeof(CommonCharacterCount) },
            {"11", typeof(IsLuckyNumber) },
            {"12", typeof(SortByHeight) },
            {"13", typeof(ReverseInParentheses) },
            {"14", typeof(AlternatingSums) },
            {"15", typeof(AddBorder) }
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
