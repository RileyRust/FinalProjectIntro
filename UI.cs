using System;

namespace HungerGamesSimulator
{
    public static class GameUI
    {
        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Hunger Games Simulator!");
            Console.WriteLine("--------------------------------------\n");
        }

        public static void DisplayContestants(string[] contestants)
        {
            Console.WriteLine("Tributes entering the arena:");
            foreach (var name in contestants)
            {
                Console.WriteLine($"- {name}");
            }
            Console.WriteLine();
        }

        public static void DisplayAction(string action)
        {
            Console.WriteLine($"Event: {action}\n");
        }
    }
}
