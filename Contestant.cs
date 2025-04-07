namespace HungerGamesSimulator
{
    public class Contestant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public int Health { get; set; }
        public int WeaponBuff { get; set; }
        public (int X, int Y) Position { get; set; }
        public string Alliance { get; set; }

        public Contestant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Health = 100;
            WeaponBuff = 0;
            Position = (0, 0);
            Alliance = "None";
        }
    }
}
