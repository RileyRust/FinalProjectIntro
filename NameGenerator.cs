using System;

namespace HungerGamesSimulator
{
    public static class NameGenerator
    {
        private static readonly string[] FirstNames = {
    "Katniss", "Peeta", "Rue", "Gale", "Finnick", "Johanna", "Cato", "Clove",
    "Thresh", "Foxface", "Glimmer", "Marvel", "Prim", "Annie", "Beetee", "Wiress",
    "Haymitch", "Effie", "Cinna", "Portia", "Seneca", "Plutarch", "Lavender", "Octavia",
    "Brutus", "Enobaria", "Cashmere", "Gloss", "Seeder", "Chaff", "Mags", "Delly",
    "Tigris", "Snow", "Coin", "Boggs", "Leeg", "Castor", "Pollux", "Messalla",
    "Lavinia", "Romulus", "Atala", "Venia", "Mitchell", "Darius", "Darius", "Madge",
    "Cray", "Ripper", "Greasy", "Peony", "Twill", "Bonnie", "Clara", "Daulton"
};


        private static readonly string[] LastNames = {
    "Everdeen", "Mellark", "Barnes", "Hawthorne", "Odair", "Mason", "Snow", "Abernathy",
    "Cresta", "Templesmith", "Flickerman", "Heavensbee", "Coin", "Trinket", "Latier",
    "Morales", "Cardew", "Paylor", "Dome", "Undersee", "Porter", "Wellington", "Dane",
    "Griffin", "Winters", "Keene", "Bellamy", "Lux", "Ridge", "Cross", "Lang",
    "Voss", "North", "Vance", "Fox", "Blythe", "Stone", "Ash", "Hart",
    "Vale", "Rowe", "Blake", "Dusk", "Thorne", "Marrow", "Hollow", "Sage"
};

        private static readonly Random rnd = new Random();

        public static string GenerateName()
        {
            string first = FirstNames[rnd.Next(FirstNames.Length)];
            string last = LastNames[rnd.Next(LastNames.Length)];
            return $"{first} {last}";
        }
    }
}
