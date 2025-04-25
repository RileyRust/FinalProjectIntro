using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public static class AreanaControl
    {
        
        private static Random rng = new Random();

        public static int TotalZones { get; set; } = 10;

        public static void AdjustArenaSize(List<Contestant> contestants)
        {
            int aliveCount = contestants.Count(c => c.Health > 0);

            if (aliveCount <= 5 && TotalZones > 5)
            {
                TotalZones = 5;
                Console.WriteLine("\n The arena shrinks! The tributes are now confined to fewer zones.");
            }
            else if (aliveCount <= 3 && TotalZones > 3)
            {
                TotalZones = 3;
                Console.WriteLine("\n The arena shrinks further! The tributes are now confined to 3 zones.");
            }
        }
         public static void CheckRunAway(Contestant c)
        {
            if (c.RanAway) return;

            double chanceToRun = 1.0 - (c.DiceRoll / 20.0);
            bool runs = rng.NextDouble() < chanceToRun;

            if (runs)
            {
                c.RanAway = true;
                int newZone;
                do
                {
                    newZone = rng.Next(1, TotalZones);
                } while (newZone == c.LocationId);

                c.LocationId = newZone;
                Console.WriteLine($"{c.FullName} flees to zone {c.LocationId}!");
            }
        }
         public static void HandleRunaways(List<Contestant> contestants)
        {
            foreach (var contestant in contestants)
            {
                contestant.LocationId = 0;
                contestant.Health = 20;
                contestant.RanAway = false;
            }
        }
          public static void RunZoneTurns(List<Contestant> contestants)
        {
            var activeZones = contestants.Where(c => c.Health > 0).Select(c => c.LocationId).Distinct(); // refactor
            foreach (int zone in activeZones)
            {
                var zoneTributes = contestants.Where(c => c.LocationId == zone && c.Health > 0).ToList();

                if (zoneTributes.Count < 2) continue; 

                var a = zoneTributes[0];
                var b = zoneTributes[1];

                if (a.Alliance != b.Alliance)
                {
                    Console.WriteLine($"\n Fight in zone {zone}!");
                    CombatStuff.RunCombatLoop(zoneTributes, zone);
                }
            }
        }
    }
}
