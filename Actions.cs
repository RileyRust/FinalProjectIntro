using System;

namespace HungerGamesSimulator
{
    public static class GameActions
    {
         private static Random rng = new Random();
        public static void StartSimulation(string[] contestants)
        {
            GameUI.DisplayContestants(contestants);

           
            string actionResult = $"{contestants[0]} found supplies in the woods.";
            GameUI.DisplayAction(actionResult);

        }
          public static int RollDice(int min, int max)
        {
            return rng.Next(min, max + 1);
        }

    }
}
