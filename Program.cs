using System;
using System.Collections.Generic;
using System.Linq;

namespace HungerGamesSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            GameUI.DisplayWelcome();

            var contestants = new List<Contestant>();
            int totalDistricts = 12;

            string answer;
            int manualDistrict = 1;

            do
            {
                Console.WriteLine("Would you like to add a contestant? (yes/no)");
                answer = Console.ReadLine()?.ToLower() ?? "no";

                if (answer == "yes")
                {
                    Console.WriteLine("What is the first name?");
                    string firstName = Console.ReadLine()?.Trim() ?? "Unknown";

                    Console.WriteLine("What is the last name?");
                    string lastName = Console.ReadLine()?.Trim() ?? "Unknown";

                    if (contestants.Count(c => c.District == manualDistrict) >= 2)
                    {
                        Console.WriteLine($"District {manualDistrict} already has 2 contestants.");
                        continue;
                    }

                    if (contestants.Any(c => c.FullName == $"{firstName} {lastName}" && c.District == manualDistrict))
                    {
                        Console.WriteLine($"Duplicate name in District {manualDistrict}: {firstName} {lastName}");
                        continue;
                    }

                    var contestant = new Contestant(firstName, lastName)
                    {
                        District = manualDistrict
                    };

                    contestants.Add(contestant);
                    if (contestants.Count(c => c.District == manualDistrict) >= 2)
                        manualDistrict++;

                    if (manualDistrict > totalDistricts)
                    {
                        Console.WriteLine("Maximum number of districts reached.");
                        break;
                    }
                }

            } while (answer == "yes");

            var districtCounts = new Dictionary<int, int>();
            foreach (var c in contestants)
            {
                if (!districtCounts.ContainsKey(c.District))
                    districtCounts[c.District] = 0;
                districtCounts[c.District]++;
            }

            for (int district = 1; district <= totalDistricts; district++)
            {
                int existingCount = districtCounts.ContainsKey(district) ? districtCounts[district] : 0;
                int toGenerate = 2 - existingCount;

                for (int i = 0; i < toGenerate; i++)
                {
                    string randomName = NameGenerator.GenerateName();
                    var nameParts = randomName.Split(' ');

                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "Unknown";

                    if (contestants.Any(c => c.FullName == $"{firstName} {lastName}" && c.District == district))
                    {
                        i--; 
                        continue;
                    }

                    var contestant = new Contestant(firstName, lastName)
                    {
                        District = district
                    };

                    contestants.Add(contestant);
                }
            }

           
            GameActions.StartSimulation(contestants);
        }
    }
}
