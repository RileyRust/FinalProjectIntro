using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            GameUI.DisplayWelcome();

            var contestants = new List<Contestant>();
            Console.WriteLine($" Would you like to create a Character?");
            Console.WriteLine($"--------------------------------------");
            Console.WriteLine($"Yes or No");
            String answer = Console.Readline()?.Trim().ToLower();
            while (answer == yes)
            {
                Console.WriteLine("What is the Characters first name"); 
                String FirstName = Console.Readline()?.Trim().ToLower();
                 Console.WriteLine("What is the Characters first name");
                String LastName = Console.Readline()?.Trim().ToLower();
                string fullName = FirstName + " " + LastName;
                contestants.Add(new Contestant(fullName)); 
                 Console.WriteLine($" Would you like to create another Character?");
                 Console.WriteLine($"--------------------------------------");
                 String answer = Console.Readline()?.Trim().ToLower();
            }
            for (int i = 0; i < 24 - contestants.Count; i++)
            {
                string randomName = NameGenerator.GenerateName();
                contestants.Add(new Contestant(randomName));
            }


            GameActions.StartSimulation(contestants);
        }
    }
}
