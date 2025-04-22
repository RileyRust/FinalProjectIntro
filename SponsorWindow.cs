using System;
using System.Collections.Generic;
using System.Linq;
namespace HungerGamesSimulator
{
    public class SponsorWindow
    {
        public static void WantstoBet(List<Contestant> contestants, Sponsor sponsor)
        {
            bool wantToBet = true;

            while (wantToBet)
            {
                Console.WriteLine("Would you like to bet? Yes/No");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "yes")
                {
                    Console.WriteLine("You're in the correct spot.");
                    BetWindow(contestants, sponsor); // pass sponsor
                }
                else if (input == "no")
                {
                    wantToBet = false;
                }
                else
                {
                    Console.WriteLine("Not a valid answer! Please type 'yes' or 'no'.\n");
                }
            }
        }


        public static void BetWindow(List<Contestant> contestants, Sponsor sponsor)
        {
            GameUI.DisplayContestants(contestants);

            Console.WriteLine("Who would you like to bet on?");
            string selectedName = Console.ReadLine();

            var selectedContestant = contestants
                .FirstOrDefault(c => c.FullName.Equals(selectedName, StringComparison.OrdinalIgnoreCase));

            if (selectedContestant != null)
            {
                Console.WriteLine($"{selectedContestant.FullName} has been found.");
                Console.WriteLine($"Your current balance is ${sponsor.Balance}.");
                Console.WriteLine("How much would you like to bet?");

                string input = Console.ReadLine();
                int amount;

                while (!int.TryParse(input, out amount) || amount <= 0 || amount > sponsor.Balance)
                {
                    Console.WriteLine($"Enter a valid number between 1 and {sponsor.Balance}:");
                    input = Console.ReadLine();
                }

                sponsor.PlaceBet(selectedContestant.FullName, amount);
            }
            else
            {
                Console.WriteLine("No such contestant found.");
            }
        }

    }
}
