

using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class TributeStuff
    {
         private static Random rng = new Random();
        public static void FormAlliances(List<Contestant> contestants)
        {
            Console.WriteLine("\n Forming alliances...");

            int numberOfAlliances = rng.Next(2, 5);
            List<string> activeAlliances = new List<string>();

            while (activeAlliances.Count < numberOfAlliances)
            {
                string name = LootStuff.AllianceNames[rng.Next(LootStuff.AllianceNames.Length)];
                if (!activeAlliances.Contains(name))
                    activeAlliances.Add(name);
            }

            Dictionary<string, List<Contestant>> allianceMembers = activeAlliances.ToDictionary(a => a, a => new List<Contestant>());
            var grouped = contestants
              .Where(c => c.Alliance != "None")
              .GroupBy(c => c.Alliance)
              .ToList();

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
        public static void ShowFallenTributes(List<Contestant> contestants)
        {
            var fallen = contestants.Where(c => c.Health <= 0).ToList();
            if (fallen.Count == 0)
            {
                Console.WriteLine("No tributes have fallen today.");
                return;
            }

            Console.WriteLine("\n Fallen Tributes:");
            foreach (var tribute in fallen)
            {
                Console.WriteLine($"- {tribute.FullName} (District {tribute.District})");
            }
        }
          public static void MoveTributes(List<Contestant> contestants)
        {
            foreach (var c in contestants.Where(c => c.Health > 0))
            {
                List<int> possibleMoves = new List<int>();
                if (c.LocationId > 0) possibleMoves.Add(c.LocationId - 1);
                if (c.LocationId < AreanaControl.TotalZones - 1) possibleMoves.Add(c.LocationId + 1);

                int newZone = possibleMoves[rng.Next(possibleMoves.Count)];
                Console.WriteLine($"{c.FullName} cautiously moves from zone {c.LocationId} to zone {newZone}.");
                c.LocationId = newZone;
            }
        }



    }
}
