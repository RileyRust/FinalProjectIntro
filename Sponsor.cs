using System;
using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class Sponsor
    {
        public int Balance { get; private set; } = 500;
        public int AmountBet { get; private set; } = 0;
        public Dictionary<string, int> Bets { get; private set; } = new Dictionary<string, int>();

        public void PlaceBet(string contestantName, int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Bet must be greater than 0.");
                return;
            }

            if (amount > Balance)
            {
                Console.WriteLine($"You only have ${Balance}. Please bet a smaller amount.");
                return;
            }

            Balance -= amount;
            AmountBet += amount;

            if (Bets.ContainsKey(contestantName))
                Bets[contestantName] += amount;
            else
                Bets[contestantName] = amount;

            Console.WriteLine($"Bet of ${amount} placed on {contestantName}. Remaining balance: ${Balance}");
        }

        public void ResolveBet(string winnerName)
        {
            if (Bets.ContainsKey(winnerName))
            {
                int winnings = Bets[winnerName] * 2;
                Balance += winnings;
                Console.WriteLine($"You won! {winnerName} survived. You earned ${winnings}!");
            }
            else
            {
                Console.WriteLine("You lost your bet.");
            }

            AmountBet = 0;
            Bets.Clear();
        }
    }
}
