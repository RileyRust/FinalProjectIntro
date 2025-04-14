namespace HungerGamesSimulator
{
    public class Contestant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Charimsa{ get; set; } = 0;

        public int District { get; set; }
        public int Health { get; set; } = 20;
        public int Hunger { get; set; } = 10;
        public int Thirst { get; set; } = 10;
        public int WeaponBuff { get; set; } = 0;
        public int DiceRoll { get; set; }
        public int LocationId { get; set; } = 0;

        public string Loot { get; set; } = "None";
        public string Alliance { get; set; } = "None";
        public bool RanAway { get; set; } = false;

        public Contestant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
