using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public static class GameUI
    {
        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Hunger Games Simulator!");
            Console.WriteLine("--------------------------------------\n");
        }

        public static void DisplayContestants(List<Contestant> contestants)
        {
            Console.WriteLine($"Tributes entering the arena ({contestants.Count}):");
            foreach (var contestant in contestants)
            {
                Console.WriteLine($"- District {contestant.District}: {contestant.FullName}");
            }
            Console.WriteLine();
        }

        public static void DisplayAction(string action)
        {
            Console.WriteLine($"Event: {action}\n");
        }
    }
}
