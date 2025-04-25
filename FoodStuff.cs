using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class FoodStuff
    {
        public static void ThirstandHunger(List<Contestant> contestants)
        {
            foreach (var contestant in contestants)
            {
                if (contestant.Health <= 0)
                    continue; // already dead, skip

                int DailyThirst = 1;
                int DailyHunger = 1;
                int HealthPenalty = 1;

                contestant.Thirst -= DailyThirst;
                contestant.Hunger -= DailyHunger;

                // Clamp values to a minimum of 0
                if (contestant.Thirst < 0) contestant.Thirst = 0;
                if (contestant.Hunger < 0) contestant.Hunger = 0;

                // Apply damage if either is 0
                if (contestant.Thirst == 0 || contestant.Hunger == 0)
                {
                    contestant.Health -= HealthPenalty;
                }

                // Print death message only when they actually die this round
                if (contestant.Health <= 0)
                {
                    if (contestant.Thirst == 0 && contestant.Hunger == 0)
                    {
                        Console.WriteLine($"{contestant.FullName} has died of exposure.");
                    }
                    else if (contestant.Thirst == 0)
                    {
                        Console.WriteLine($"{contestant.FullName} has died of thirst.");
                    }
                    else if (contestant.Hunger == 0)
                    {
                        Console.WriteLine($"{contestant.FullName} has died of hunger.");
                    }
                }
            }
        }
    }
}
