using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class LootStuff
    {
        private static Random rng = new Random();

        public static readonly string[] CommonLoot = { "Bread", "Canteen", "Bandages", "Rock", "Apple", "Ripped Shirt", "Club", "Slingshot", "Caprisun", "Knife" };
        public static readonly string[] RareLoot = { "Spear", "Medkit", "Bow", "Sword", "Trident", "Jerky", "Chocolate Milk" };
        public static readonly string[] AllianceNames = { "Wolf Pack", "Stealth Squad", "Fire Alliance", "Nightwatchers" };

        private static readonly Dictionary<string, int> WeaponBuffs = new Dictionary<string, int>
        {
            { "Rock", 1 },
            { "Club", 2 },
            { "Slingshot", 3 },
            { "Knife", 3 },
            { "Spear", 4 },
            { "Bow", 5 },
            { "Sword", 6 },
            { "Trident", 7 }
        };
        private static readonly Dictionary<string, int> HealthBuffs = new Dictionary<string, int>
        {
             { "Ripped Shirt", 1 },
             { "Bandage", 5 },
            { "Medkit", 10 }
        };
        private static readonly Dictionary<string, int> FoodBuffs = new Dictionary<string, int>
        {
            {"Apple", 1 },
            {"Bread", 3 },
            {"Jerky", 5 }
        };
        private static readonly Dictionary<string, int> ThirstBuffs = new Dictionary<string, int>
        {
            {"Caprisun", 1},
            {"Canteen", 3},
            {"Chocolate Milk", 5}
        };
        public static int RollDice(int min, int max)
        {
            return rng.Next(min, max + 1);
        }

        public static void CarePackage(List<Contestant> contestants)
        {
            double checkForCarePackage = 0.90;
            double secondCheckforCarePackage = 0.50;
            foreach (var contestant in contestants)
            {
                string previousLoot = contestant.Loot;


                if ((contestant.Charisma / 20.0) >= checkForCarePackage && contestant.GotLoot == false && (contestant.Health > 0))
                {
                    string GeneratedLoot = RareLoot[rng.Next(RareLoot.Length)];
                    contestant.Loot = GeneratedLoot;
                    string[] carePackageMessages = {
    $"{contestant.FullName} looks to the sky as a silver parachute descends, glinting in the sunlight. Inside: a {GeneratedLoot}.",
    $"A soft whir cuts through the silence. {contestant.FullName} rushes to grab the care package before others notice. They've secured a {GeneratedLoot}.",
    $"Cheers erupt from hidden sponsors as {contestant.FullName} earns a {GeneratedLoot} — their charisma has paid off.",
    $"From the sky, a gift floats down gently. {contestant.FullName} clutches the {GeneratedLoot}, a small edge in a deadly game.",
    $"{contestant.FullName} hears the ping of a descending drone. With trembling hands, they open the container to find a {GeneratedLoot}.",
    $"A sponsor’s favor arrives at just the right time. {contestant.FullName} receives a {GeneratedLoot}, their eyes wide with hope."
};


                    Console.WriteLine(carePackageMessages[rng.Next(carePackageMessages.Length)]);
                    contestant.GotLoot = true;
                }
                else if (contestant.Charisma / 80.0 >= secondCheckforCarePackage && contestant.GotLootSecond == false && (contestant.Health > 0))
                {
                    string GeneratedLoot = RareLoot[rng.Next(RareLoot.Length)];
                    contestant.Loot = GeneratedLoot;
                    Console.WriteLine($"{contestant.FullName} Received a {GeneratedLoot}");
                    contestant.GotLootSecond = true;

                }
                else
                {
                    int Charismabuff = RollDice(1, 5);
                    contestant.Charisma += Charismabuff;
                }

                if (HealthBuffs.ContainsKey(contestant.Loot) &&
     !contestant.AppliedHealthBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Health += HealthBuffs[contestant.Loot];
                    contestant.AppliedHealthBuffs[contestant.Loot] = 1;
                }

                if (FoodBuffs.ContainsKey(contestant.Loot) &&
                    !contestant.AppliedFoodBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Hunger += FoodBuffs[contestant.Loot];
                    contestant.AppliedFoodBuffs[contestant.Loot] = 1;
                }

                if (ThirstBuffs.ContainsKey(contestant.Loot) &&
                    !contestant.AppliedThirstBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Thirst += ThirstBuffs[contestant.Loot];
                    contestant.AppliedThirstBuffs[contestant.Loot] = 1;
                }



            }
        }
        public static void AssignLoot(List<Contestant> contestants)
        {
            Console.WriteLine("\n Loot scramble...");
            foreach (var contestant in contestants)
            {
                string previousLoot = contestant.Loot;
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


                if (HealthBuffs.ContainsKey(contestant.Loot) &&
     !contestant.AppliedHealthBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Health += HealthBuffs[contestant.Loot];
                    contestant.AppliedHealthBuffs[contestant.Loot] = 1;
                }

                if (FoodBuffs.ContainsKey(contestant.Loot) &&
                    !contestant.AppliedFoodBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Hunger += FoodBuffs[contestant.Loot];
                    contestant.AppliedFoodBuffs[contestant.Loot] = 1;
                }

                if (ThirstBuffs.ContainsKey(contestant.Loot) &&
                    !contestant.AppliedThirstBuffs.ContainsKey(contestant.Loot))
                {
                    contestant.Thirst += ThirstBuffs[contestant.Loot];
                    contestant.AppliedThirstBuffs[contestant.Loot] = 1;
                }



            }
        }


    }
}
