using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace HungerGamesSimulator
{
    public static class GameActions
    {
        private static Random rng = new Random();

        public static void StartSimulation(List<Contestant> contestants)
        {

            contestants = contestants
                .GroupBy(c => (c.FullName, c.District))
                .Select(g => g.First())
                .ToList();
            GameUI.DisplayContestants(contestants);
            Sponsor sponsor = new Sponsor();
            SponsorWindow.WantstoBet(contestants, sponsor);

            int daycount = 0;

            TributeStuff.FormAlliances(contestants);
            LootStuff.AssignLoot(contestants);
            AreanaControl.HandleRunaways(contestants);

            Console.WriteLine("\n Loot Summary:");
            contestants.Where(c => c.Loot != "None").ToList().ForEach(c =>
            {
                if (new[] { "Knife", "Spear", "Bow", "Trident", "Sword", "Rock", "Club", "Slingshot" }.Contains(c.Loot))
                    Console.WriteLine($"{c.FullName} (District {c.District}) - Loot: {c.Loot}, Weapon Buff: {c.WeaponBuff}");
                else
                    Console.WriteLine($"{c.FullName} (District {c.District}) - Loot: {c.Loot}");
            });

            Console.WriteLine("\n Cornucopia Battle Begins!");
            CombatStuff.RunCombatLoop(contestants, 0);

            Console.WriteLine("\n Night falls over the arena...");
            daycount++;
            TributeStuff.ShowFallenTributes(contestants);
            LootStuff.CarePackage(contestants);

            Console.WriteLine("\nPress any key to continue to the next day...");
            Console.ReadKey();

            while (contestants.Count(c => c.Health > 0) > 1)
            {
                Console.WriteLine("\n A new day dawns...");

                var aliveBefore = contestants.Where(c => c.Health > 0).Select(c => c.FullName).ToHashSet();

                AreanaControl.AdjustArenaSize(contestants);
                AreanaControl.RunZoneTurns(contestants);

                Console.WriteLine("\n Tributes move through the arena...");
                TributeStuff.MoveTributes(contestants);

                for (int zone = 0; zone < AreanaControl.TotalZones; zone++)
                {
                    CombatStuff.RunCombatLoop(contestants, zone);
                }

                int aliveCount = contestants.Count(c => c.Health > 0);
                if (aliveCount <= 6)
                {
                    var grouped = contestants
                        .Where(c => c.Health > 0 && c.Alliance != "None")
                        .GroupBy(c => c.Alliance);

                    foreach (var alliance in grouped)
                    {
                        if (alliance.Count() >= 3)
                        {
                            foreach (var member in alliance)
                            {
                                member.Alliance = "None";
                            }
                            Console.WriteLine($"The '{alliance.Key}' alliance has dissolved under pressure!");
                        }
                    }
                }

                if (contestants.Count(c => c.Health > 0) == 2)
                {
                    Console.WriteLine("\n Final Battle Begins between the last two tributes!");
                    var finalTwo = contestants.Where(c => c.Health > 0).ToList();
                    CombatStuff.RunFinalCombat(finalTwo[0], finalTwo[1]);
                    break;
                }

                Console.WriteLine("\n Night falls...");

                var newDeaths = contestants
                    .Where(c => c.Health <= 0 && aliveBefore.Contains(c.FullName))
                    .ToList();

                if (newDeaths.Count == 0)
                {
                    Console.WriteLine("No tributes have fallen today.");
                }
                else
                {
                    Console.WriteLine("\n Fallen Tributes:");
                    foreach (var tribute in newDeaths)
                    {
                        Console.WriteLine($"- {tribute.FullName} (District {tribute.District})");
                    }
                }

                LootStuff.CarePackage(contestants);
                FoodStuff.ThirstandHunger(contestants);

                daycount++;
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

            var winner = contestants.FirstOrDefault(c => c.Health > 0);
            if (winner != null)
            {
                Console.WriteLine($"\n{winner.FullName} (District {winner.District}) is the victor of the Hunger Games!");
                sponsor.ResolveBet(winner.FullName);
            }
        }

        public static int RollDice(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
    }
}
