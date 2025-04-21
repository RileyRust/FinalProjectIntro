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
            while (contestants.Count(c => c.LocationId == zone && c.Health > 0) > 1)
            {
                var eligiblefighters = contestants
                    .Where(c => c.LocationId == zone && c.Health > 0)
                    .ToList();

                if (eligiblefighters.Count < 2)
                    break; // Not enough fighters to continue

                var fighters = eligiblefighters.OrderBy(c => rng.Next()).ToList();

                var attacker = fighters[0];
                var defender = fighters[1];

                // Safety check — just in case
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
                    Console.WriteLine($" {winner.FullName} has killed {loser.FullName}.");
                    winner.Charisma += 5;
                    Console.WriteLine($"                              {winner.FullName}'s Charisma increased to {winner.Charisma}");
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
                Console.WriteLine($" {loser.FullName} has fallen! {tribute1.FullName} wins the final battle!");
            }
            else
            {
                Console.WriteLine($" {tribute2.FullName} survives with {tribute2.Health} health left.");
            }
        }


    }
}
