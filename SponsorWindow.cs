using System.Collections.Generic;

namespace HungerGamesSimulator
{
    public class SponsorWindow
    {

        public static void WantstoBet(List<Contestant> contestants)
        {
            bool WantstoBetonTribute = true;
            while (WantstoBetonTribute == true)
            {
                Console.WriteLine("Would you Like to Bet? Yes/No");
                string Answer = Console.ReadLine();
                string RealAnswer = Answer.ToLower();
                if (RealAnswer == "yes")
                {
                    Console.WriteLine("you are in the corerct spot");
                    BetWindow(contestants);
                }
                else if (RealAnswer == "no")
                {
                    WantstoBetonTribute = false;
                }
                else
                {
                    Console.WriteLine("Not a valid answer!!!!\n");
                }
            }
        }

        public static void BetWindow(List<Contestant> contestants)
        {
            int Money = 100; 
            GameUI.DisplayContestants(contestants);
            Console.WriteLine("Who would you like to bet on");
            string selectedName = Console.ReadLine();
            Contestant selectedContestant = contestants
      .FirstOrDefault(c => c.FullName.Equals(selectedName, StringComparison.OrdinalIgnoreCase));

            if (selectedContestant != null)
            {
                Console.WriteLine($"{selectedContestant.FullName} has been found. Good luck!");
                Placebet(selectedContestant.FullName, Money); 
            }
            else
            {
                Console.WriteLine("No such contestant found.");
            }



        }
        public static void Placebet(string contestantName, int money)
        {
         Console.WriteLine(contestantName);
          Console.WriteLine(money);
          //works
        }


    }
}
