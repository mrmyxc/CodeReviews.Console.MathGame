namespace csa_maths_game
{
    public static class HighScore
    {
        private static readonly int MaxScores = 5;
        static List<GameScore> highScores = new List<GameScore>(MaxScores);
        static readonly Dictionary<int, string> Positional = new Dictionary<int, string>()
        {
            { 1, "1st" },
            { 2, "2nd" },
            { 3, "3rd" },
            { 4, "4th" },
            { 5, "5th" }
        };

        public static void Add(GameScore gameScore)
        {
            highScores.Add(gameScore);
            highScores.Sort();
            if (highScores.Count > MaxScores)
            {
                highScores.RemoveAt(MaxScores);
            }
        }
        public static void DisplayHighScores()
        {
            if (highScores.Count == 0)
            {
                Console.WriteLine("No scores recorded");
            }
            else
            {
                int i = 1;
                foreach (var score in highScores)
                {
                    Console.WriteLine("{0} : {1} in {2}", Positional[i], score.Score, score.GameName);
                    i++;
                }
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
