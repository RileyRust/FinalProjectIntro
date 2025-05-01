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
    "Lavinia", "Romulus", "Atala", "Venia", "Mitchell", "Darius", "Madge", "Cray",
    "Ripper", "Greasy", "Peony", "Twill", "Bonnie", "Clara", "Daulton", "Cressida",
    "Zephyra", "Cassian", "Lyric", "Aurelia", "Tavian", "Nero", "Lyra", "Seraphine",
    "Vesper", "Thorne", "Isolde", "Caius", "Drusus", "Olenna", "Rhea", "Kael",
    "Sable", "Maelis", "Corvin", "Nyx", "Elio", "Sorrel", "Darien", "Juno",
    "Alaric", "Tycho", "Liora", "Bellamy", "Orin", "Tamsin", "Zarek", "Mira",
    "Lazaro", "Blythe", "Cassio", "Ophelia", "Thalia", "Zinnia", "Ignis", "Branwen",
    "Iris", "Altair", "Selene", "Fenris"
};


      private static readonly string[] LastNames = {
    "Everdeen", "Mellark", "Barnes", "Hawthorne", "Odair", "Mason", "Snow", "Abernathy",
    "Cresta", "Templesmith", "Flickerman", "Heavensbee", "Coin", "Trinket", "Latier",
    "Morales", "Cardew", "Paylor", "Dome", "Undersee", "Porter", "Wellington", "Dane",
    "Griffin", "Winters", "Keene", "Bellamy", "Lux", "Ridge", "Cross", "Lang",
    "Voss", "North", "Vance", "Fox", "Blythe", "Stone", "Ash", "Hart",
    "Vale", "Rowe", "Blake", "Dusk", "Thorne", "Marrow", "Hollow", "Sage",
    "Quell", "Brambles", "Loam", "Fallow", "Nightshade", "Mire", "Shale", "Wolfe",
    "Drake", "Slate", "Thistle", "Yarrow", "Greaves", "Flint", "Briar", "Frost",
    "Ashen", "Barrow", "Mourne", "Gale", "Pike", "Quinn", "Wren", "Lark",
    "Stroud", "Fern", "Bane", "Cormac", "Grimm", "Rook", "Tarn", "Holloway",
    "Raven", "Silas", "Dray", "Thornefield", "Sallow", "Granger", "Sorrell", "Vale",
    "Bexley", "Whitt", "Blackthorn", "Crowe", "Vellum", "Locke", "Noir", "Graye",
    "Ironwood", "Tarnish", "Fable", "Ravel", "Craven", "Duskwell", "Branlow", "Fallon"
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
