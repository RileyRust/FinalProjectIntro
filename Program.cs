using System;

namespace HungerGamesSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            GameUI.DisplayWelcome();

            // Initialize contestants (you can expand this)
            string[] contestants = { "Katniss", "Peeta", "Rue", "Cato" };

            // Start simulation
            GameActions.StartSimulation(contestants);
        }
    }
}
