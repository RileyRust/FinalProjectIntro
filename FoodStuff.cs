using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class FoodStuff
    {
         public static void ThirstandHunger(List<Contestant> contestants)
        {
            foreach (var contestant in contestants)
            {
                int DailyThirst = 0; // set all these to zero to figure out a way so they don't all die from stuff
                int DailyHunger = 0;
                int dyingfromstuff = 0;

                contestant.Thirst = contestant.Thirst - DailyThirst;
                contestant.Hunger = contestant.Hunger - DailyHunger;
                if (contestant.Hunger == 0 || contestant.Thirst == 0)
                {
                    contestant.Health = contestant.Health - dyingfromstuff;
                }
                if (contestant.Thirst <= 0 && contestant.Hunger <= 0 && contestant.Health <= 0)
                {
                    Console.WriteLine($"{contestant.FullName} Has Died of Exposure");

                }
                else if (contestant.Thirst <= 0 && contestant.Health <= 0)
                {
                    Console.WriteLine($"{contestant.FullName} Has Died of Thirst");

                }
                else if (contestant.Hunger <= 0 && contestant.Health <= 0)
                {
                    Console.WriteLine($"{contestant.FullName} Has Died of Hunger");

                }
            }
        }
        
    }
}
