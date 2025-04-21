using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class Contestant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public int Charisma { get; set; } = 0;
        public int District { get; set; }
        public int Health { get; set; } = 20;
        public int Hunger { get; set; } = 10;
        public int Thirst { get; set; } = 10;
        public int WeaponBuff { get; set; } = 0;
        public int DiceRoll { get; set; }
        public int LocationId { get; set; } = 0;
        public bool GotLoot { get; set; } = false;
        public bool GotLootSecond { get; set; } = false;
        public string Loot { get; set; } = "None";
        public string Alliance { get; set; } = "None";
        public bool RanAway { get; set; } = false;

        public string LastBuffedLoot { get; set; } = "None";
        public Dictionary<string, int> AppliedFoodBuffs { get; set; } = new();
        public Dictionary<string, int> AppliedHealthBuffs { get; set; } = new();
        public Dictionary<string, int> AppliedThirstBuffs { get; set; } = new();







        public Contestant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
