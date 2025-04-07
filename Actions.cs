using System;

namespace HungerGamesSimulator
{
    public static class GameActions
    {
        public static void StartSimulation(string[] contestants)
        {
            GameUI.DisplayContestants(contestants);

            // Example round action
            string actionResult = $"{contestants[0]} found supplies in the woods.";
            GameUI.DisplayAction(actionResult);

            // More logic will go here as you build it out
        }
    }
}
