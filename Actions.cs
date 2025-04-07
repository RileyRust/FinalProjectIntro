using System;
using System.Collections.Generic;
using System.Linq;

namespace HungerGamesSimulator
{
    public static class GameActions
    {
        private static Random rng = new Random();

        private static readonly string[] CommonLoot = { "Bread", "Canteen", "Bandages" };
        private static readonly string[] RareLoot = { "Knife", "Spear", "Medkit", "Bow" };
        private static readonly string[] AllianceNames = { "Wolf Pack", "Stealth Squad", "Fire Alliance", "Nightwatchers" };

        private static readonly Dictionary<string, int> WeaponBuffs = new Dictionary<string, int>
        {
            { "Knife", 3 },
            { "Spear", 7 },
            { "Bow", 5 }
        };

        private static int TotalZones = 10;

        public static void StartSimulation(List<Contestant> contestants)
        {
            GameUI.DisplayContestants(contestants);

            FormAlliances(contestants);
            AssignLoot(contestants);
            HandleRunaways(contestants);

            Console.WriteLine("\n🎒 Loot Summary:");
            contestants.Where(c => c.Loot != "None").ToList().ForEach(c =>
            {
                if (new[] { "Knife", "Spear", "Bow" }.Contains(c.Loot))
                    Console.WriteLine($"{c.FullName} (District {c.District}) - Loot: {c.Loot}, Weapon Buff: {c.WeaponBuff}");
                else
                    Console.WriteLine($"{c.FullName} (District {c.District}) - Loot: {c.Loot}");
            });

            Console.WriteLine("\n🌪️ Cornucopia Battle Begins!");
            RunCombatLoop(contestants, 0);

            Console.WriteLine("\n🌒 Night falls over the arena...");
            ShowFallenTributes(contestants);

            Console.WriteLine("\nPress any key to continue to the next day...");
            Console.ReadKey();

            while (contestants.Count(c => c.Health > 0) > 1)
            {
                Console.WriteLine("\n🌞 A new day dawns...");

                var aliveBefore = contestants.Where(c => c.Health > 0).Select(c => c.FullName).ToHashSet();
                
                AdjustArenaSize(contestants);

                // Run combat or simulate actions for all tributes
                RunZoneTurns(contestants);

                // If enough tributes are alive, proceed with movements and alliance breakups
                Console.WriteLine("\n🌍 Tributes move through the arena...");
                MoveTributes(contestants);

                // Breakup logic if 6 or fewer tributes remain
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
                            Console.WriteLine($"⚠️ The '{alliance.Key}' alliance has dissolved under pressure!");
                        }
                    }
                }
                if (contestants.Count(c => c.Health > 0) == 2)
                {
                    Console.WriteLine("\n🔥 Final Battle Begins between the last two tributes!");

                    // Get the two remaining tributes
                    var finalTwo = contestants.Where(c => c.Health > 0).ToList();

                    // Run the final combat between them
                    RunFinalCombat(finalTwo[0], finalTwo[1]);

                    // End the simulation after the final battle
                    break;
                }

                // Check and run combat for any remaining tributes in each zone
                Console.WriteLine("\n🌒 Night falls...");

                var newDeaths = contestants
                    .Where(c => c.Health <= 0 && aliveBefore.Contains(c.FullName))
                    .ToList();

                if (newDeaths.Count == 0)
                {
                    Console.WriteLine("No tributes have fallen today.");
                }
                else
                {
                    Console.WriteLine("\n🕯️ Fallen Tributes:");
                    foreach (var tribute in newDeaths)
                    {
                        Console.WriteLine($"- {tribute.FullName} (District {tribute.District})");
                    }
                }

                // Add a keypress to continue after a night phase
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();



            }


            var winner = contestants.FirstOrDefault(c => c.Health > 0);
            if (winner != null)
            {
                Console.WriteLine($"\n🏆 {winner.FullName} (District {winner.District}) is the victor of the Hunger Games!");
            }
        }


        private static void FormAlliances(List<Contestant> contestants)
        {
            Console.WriteLine("\n🤝 Forming alliances...");

            int numberOfAlliances = rng.Next(2, 5);
            List<string> activeAlliances = new List<string>();

            while (activeAlliances.Count < numberOfAlliances)
            {
                string name = AllianceNames[rng.Next(AllianceNames.Length)];
                if (!activeAlliances.Contains(name))
                    activeAlliances.Add(name);
            }

            Dictionary<string, List<Contestant>> allianceMembers = activeAlliances.ToDictionary(a => a, a => new List<Contestant>());
            List<Contestant> shuffled = contestants.OrderBy(_ => rng.Next()).ToList();
            int index = 0;



            for (; index < shuffled.Count; index++)
            {
                var tribute = shuffled[index];
                if (rng.NextDouble() < 0.4)
                {
                    var eligible = allianceMembers.Where(kvp => kvp.Value.Count < 4).Select(kvp => kvp.Key).ToList();
                    if (eligible.Count > 0)
                    {
                        string chosen = eligible[rng.Next(eligible.Count)];
                        tribute.Alliance = chosen;
                        allianceMembers[chosen].Add(tribute);
                        GameUI.DisplayAction($"{tribute.FullName} joins the '{chosen}' alliance!");
                        continue;
                    }
                }
            }
        }

        private static void AssignLoot(List<Contestant> contestants)
        {
            Console.WriteLine("\n🧺 Loot scramble...");
            foreach (var contestant in contestants)
            {
                contestant.DiceRoll = RollDice(1, 20);
                double rollFactor = contestant.DiceRoll / 20.0;

                if (contestant.DiceRoll == 20)
                {
                    contestant.Loot = RareLoot[rng.Next(RareLoot.Length)];
                }
                else if (contestant.DiceRoll == 1)
                {
                    contestant.Loot = "None";
                }
                else if (rollFactor > 0.75)
                {
                    contestant.Loot = RareLoot[rng.Next(RareLoot.Length)];
                }
                else if (rollFactor > 0.3)
                {
                    contestant.Loot = CommonLoot[rng.Next(CommonLoot.Length)];
                }
                else
                {
                    contestant.Loot = "None";
                }

                if (WeaponBuffs.ContainsKey(contestant.Loot))
                    contestant.WeaponBuff += WeaponBuffs[contestant.Loot];
            }
        }

        private static void HandleRunaways(List<Contestant> contestants)
        {
            foreach (var contestant in contestants)
            {
                contestant.LocationId = 0;
                contestant.Health = 20;
                contestant.RanAway = false;
            }
        }

        private static void RunCombatLoop(List<Contestant> contestants, int zone)
        {
            while (contestants.Count(c => c.LocationId == zone && c.Health > 0) > 1)
            {
                var fighters = contestants.Where(c => c.LocationId == zone && c.Health > 0).OrderBy(_ => rng.Next()).ToList();

                var attacker = fighters[0];
                var defender = fighters[1];

                int roll1 = RollDice(1, 20) + attacker.WeaponBuff;
                int roll2 = RollDice(1, 20) + defender.WeaponBuff;

                int damage = Math.Max(1, Math.Abs(roll1 - roll2));
                var loser = roll1 > roll2 ? defender : attacker;
                loser.Health -= damage;

                Console.WriteLine($"{attacker.FullName} and {defender.FullName} clash! {loser.FullName} takes {damage} damage.");

                if (loser.Health <= 0)
                {
                    Console.WriteLine($"💀 {loser.FullName} has died in zone {zone}.");
                }
                else
                {
                    CheckRunAway(loser);
                }
            }
        }

        private static void RunZoneTurns(List<Contestant> contestants)
        {
            var activeZones = contestants.Where(c => c.Health > 0).Select(c => c.LocationId).Distinct();
            foreach (int zone in activeZones)
            {
                var zoneTributes = contestants.Where(c => c.LocationId == zone && c.Health > 0).ToList();

                if (zoneTributes.Count < 2) continue; // Combat only happens if there are at least two tributes

                var a = zoneTributes[0];
                var b = zoneTributes[1];

                if (a.Alliance != b.Alliance)
                {
                    Console.WriteLine($"\n🗡️ Fight in zone {zone}!");
                    RunCombatLoop(zoneTributes, zone);
                }
                else if (a.Alliance == "None")
                {
                    string newAlliance = "ZoneAllies" + zone;
                    a.Alliance = newAlliance;
                    b.Alliance = newAlliance;
                    Console.WriteLine($"🫱🏼‍🫲🏼 {a.FullName} and {b.FullName} formed a new alliance in zone {zone}!");
                }
            }
        }


        private static void ShowFallenTributes(List<Contestant> contestants)
        {
            var fallen = contestants.Where(c => c.Health <= 0).ToList();
            if (fallen.Count == 0)
            {
                Console.WriteLine("No tributes have fallen today.");
                return;
            }

            Console.WriteLine("\n🕯️ Fallen Tributes:");
            foreach (var tribute in fallen)
            {
                Console.WriteLine($"- {tribute.FullName} (District {tribute.District})");
            }
        }

        private static void CheckRunAway(Contestant c)
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

        public static int RollDice(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
        private static void MoveTributes(List<Contestant> contestants)
        {
            foreach (var c in contestants.Where(c => c.Health > 0))
            {
                List<int> possibleMoves = new List<int>();
                if (c.LocationId > 0) possibleMoves.Add(c.LocationId - 1);
                if (c.LocationId < TotalZones - 1) possibleMoves.Add(c.LocationId + 1);

                int newZone = possibleMoves[rng.Next(possibleMoves.Count)];
                Console.WriteLine($"{c.FullName} cautiously moves from zone {c.LocationId} to zone {newZone}.");
                c.LocationId = newZone;
            }
        }
        private static void RunFinalCombat(Contestant tribute1, Contestant tribute2)
        {
            Console.WriteLine($"\n💥 {tribute1.FullName} vs. {tribute2.FullName}");

            // Each tribute rolls for attack
            int roll1 = RollDice(1, 20) + tribute1.WeaponBuff;
            int roll2 = RollDice(1, 20) + tribute2.WeaponBuff;

            // Calculate damage
            int damage = Math.Max(1, Math.Abs(roll1 - roll2));
            var loser = roll1 > roll2 ? tribute2 : tribute1;
            loser.Health -= damage;

            Console.WriteLine($"{tribute1.FullName} rolls {roll1} and {tribute2.FullName} rolls {roll2}");
            Console.WriteLine($"{loser.FullName} takes {damage} damage!");

            if (loser.Health <= 0)
            {
                Console.WriteLine($"💀 {loser.FullName} has fallen! {tribute1.FullName} wins the final battle!");
            }
            else
            {
                Console.WriteLine($"⚔️ {tribute2.FullName} survives with {tribute2.Health} health left.");
            }
        }
        private static void AdjustArenaSize(List<Contestant> contestants)
        {
            int aliveCount = contestants.Count(c => c.Health > 0);

            if (aliveCount <= 5 && TotalZones > 5) // Shrink to 5 zones when 5 tributes are left
            {
                TotalZones = 5;
                Console.WriteLine("\n🌍 The arena shrinks! The tributes are now confined to fewer zones.");
            }
            else if (aliveCount <= 3 && TotalZones > 3) // Shrink to 3 zones when 3 tributes are left
            {
                TotalZones = 3;
                Console.WriteLine("\n🌍 The arena shrinks further! The tributes are now confined to 3 zones.");
            }
        }



    }
}