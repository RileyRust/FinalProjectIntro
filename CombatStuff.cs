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
               
                Console.WriteLine($"{weaponKillMessages[winner.Loot]}");
                winner.Charisma += 5;
                Console.WriteLine($" {winner.FullName}'s Charisma increased to {winner.Charisma}");
            }
            else
            {
               
                Console.WriteLine($"{winner.FullName} killed {loser.FullName} with their unknown weapon, bringing an end to the fight.");
                winner.Charisma += 5;
                Console.WriteLine($" {winner.FullName}'s Charisma increased to {winner.Charisma}");
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

            int roll1 = RollDice(1, 20) + tribute1.WeaponBuff;
            int roll2 = RollDice(1, 20) + tribute2.WeaponBuff;

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

         public static void RunThreeFinalCombat(Contestant tribute1, Contestant tribute2, Contestant tribute3)
        {
            Console.WriteLine($"\n {tribute1.FullName} vs. {tribute2.FullName}");

            int roll1 = RollDice(1, 20) + tribute1.WeaponBuff;
            int roll2 = RollDice(1, 20) + tribute2.WeaponBuff;

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
