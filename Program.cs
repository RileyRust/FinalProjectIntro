using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            GameUI.DisplayWelcome();

            var contestants = new List<Contestant>();
            int totalDistricts = 12;

            for (int district = 1; district <= totalDistricts; district++)
            {
                for (int i = 0; i < 2; i++)
                {
                    string randomName = NameGenerator.GenerateName();
                    var nameParts = randomName.Split(' ');

                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "Unknown";

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
