using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public static class CombatStuff
    {
        private static Random rng = new Random();
        public static int RollDice(int min, int max)
        {
            return rng.Next(min, max + 1);
        }
        public static void RunCombatLoop(List<Contestant> contestants, int zone)
        {
            var weaponKillMessages = new Dictionary<string, string>
    {
        { "Spear", "{winner.FullName} killed {loser.FullName} with a spear to the chest, ending their struggle." },
        { "Bow", "{winner.FullName} pierced {loser.FullName}'s heart with a swift arrow, the bowstring's snap marking the end of their fierce battle." },
        { "Sword", "{winner.FullName} drove their sword into {loser.FullName}'s side, the blade singing through the air before it silenced the clash of battle once and for all." },
        { "Trident", "{winner.FullName} thrust the trident with lethal precision, its three prongs sinking deep into {loser.FullName}'s chest, ending the contest in a single, brutal strike." },
        { "Rock", "{winner.FullName} snatched up a sharp-edged rock and hurled it with deadly aim, the stone crashing into {loser.FullName}'s temple and ending their fight with brutal finality." },
        { "Club", "{winner.FullName} swung the heavy club in a savage arc, its force smashing into {loser.FullName}'s skull, and the battle was over in an instant." },
        { "Slingshot", "{winner.FullName} launched a stone from the slingshot, the projectile finding its mark in {loser.FullName}'s throat, silencing their defiance with a swift, unexpected blow." },
        { "Knife", "{winner.FullName} plunged the gleaming knife into {loser.FullName}'s side with swift, silent precision, the blade ending their struggle in a flash of deadly steel." }
    };

            while (contestants.Count(c => c.LocationId == zone && c.Health > 0) > 1)
            {
                var eligiblefighters = contestants
                    .Where(c => c.LocationId == zone && c.Health > 0)
                    .ToList();

                if (eligiblefighters.Count < 2)
                    break;

                var fighters = eligiblefighters.OrderBy(c => rng.Next()).ToList();

                var attacker = fighters[0];
                var defender = fighters[1];


                if (attacker == defender)
                    continue;

                int roll1 = RollDice(1, 20) + attacker.WeaponBuff;
                int roll2 = RollDice(1, 20) + defender.WeaponBuff;

                int damage = Math.Max(1, Math.Abs(roll1 - roll2));
                Contestant winner;
                Contestant loser;

                if (roll1 > roll2)
                {
                    winner = attacker;
                    loser = defender;
                }
                else
                {
                    winner = defender;
                    loser = attacker;
                }

                loser.Health -= damage;

                Console.WriteLine($"{attacker.FullName} and {defender.FullName} clash! {loser.FullName} takes {damage} damage.");


                if (loser.Health <= 0)
                {

                    if (weaponKillMessages.ContainsKey(winner.Loot))
                    {

                        string killMessage = weaponKillMessages[winner.Loot]
             .Replace("{winner.FullName}", winner.FullName)
             .Replace("{loser.FullName}", loser.FullName);

                        Console.WriteLine(killMessage);
                        winner.Charisma += 5;
                        Console.WriteLine($" {winner.FullName}'s Charisma increased to {winner.Charisma}");
                    }
                    else
                    {

                        Console.WriteLine($"{winner.FullName} killed {loser.FullName} with their bare hands");
                        winner.Charisma += 6;
                        Console.WriteLine($"{winner.FullName}'s Charisma increased to {winner.Charisma}");

                        if(loser.Loot != null)
                        {
                         Console.WriteLine($"{loser.FullName} dropped {loser.Loot} to the ground blood coating it");
                        }

                    }
                    

                }
                else
                {

                    AreanaControl.CheckRunAway(loser);
                }
            }
        }

        public static void RunFinalCombat(Contestant tribute1, Contestant tribute2)
        {
            Console.WriteLine($"\n {tribute1.FullName} vs. {tribute2.FullName}");

            while (tribute1.Health > 0 && tribute2.Health > 0)
            {
                int roll1 = RollDice(1, 20) + tribute1.WeaponBuff;
                int roll2 = RollDice(1, 20) + tribute2.WeaponBuff;

                int damage = Math.Max(1, Math.Abs(roll1 - roll2));

                Contestant winner, loser;
                if (roll1 > roll2)
                {
                    winner = tribute1;
                    loser = tribute2;
                }
                else
                {
                    winner = tribute2;
                    loser = tribute1;
                }

                loser.Health -= damage;

                Console.WriteLine($"{tribute1.FullName} rolls {roll1} and {tribute2.FullName} rolls {roll2}");
                Console.WriteLine($"{loser.FullName} takes {damage} damage!");

                if (loser.Health <= 0)
                {
                    winner.Charisma += 5;

                    if (winner.District == loser.District)
                    {
                        Console.WriteLine($"\n{winner.FullName} drops their weapon, falling to their knees beside {loser.FullName}'s body.");
                        Console.WriteLine("Tears stream down their face as the weight of taking a childhood friend’s life crashes down.");
                        Console.WriteLine("They whisper, 'We were never meant to be enemies...'");
                    }
                    else
                    {
                        string[] finalMessages = {
                    $"The cannon fires. The Capitol falls silent. {winner.FullName} stands alone—battered, bloodied, but unbroken. The Hunger Games are over.",
                    $"As the final breath escapes {loser.FullName}, {winner.FullName} collapses to their knees, victorious. The arena has a new champion.",
                    $"The sky erupts with fireworks. A hovercraft descends. {winner.FullName} raises their weapon high. This year's Hunger Games has its victor.",
                    $"The forest hushes, the mockingjays stop singing. {winner.FullName} stares into the distance, knowing they survived the unthinkable.",
                    $"With one final blow, it's done. {winner.FullName} is the last tribute standing. Against all odds, they have won the Hunger Games."
                };
                        Console.WriteLine("\n" + finalMessages[rng.Next(finalMessages.Length)]);
                    }

                    Console.WriteLine($"\n{winner.FullName}'s Charisma increased to {winner.Charisma}");
                }
                else
                {
                    Console.WriteLine($" {loser.FullName} survives with {loser.Health} health left.");
                    Console.WriteLine("The battle continues...\n");
                }
            }
        }


    }

}

