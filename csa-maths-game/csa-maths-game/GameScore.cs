using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace csa_maths_game
{
    public class GameScore : IComparable<GameScore>
    {
        public int Score { get; private set; }

        public string GameName { get; private set; }

        public GameScore(int score, string name)
        {
            Score = score;
            GameName = name;
        }

        public int CompareTo(GameScore? other)
        {
            if (other is null) return 1;
            if (Score > other.Score) return -1;
            if (Score < other.Score) return 1;
            return 0;
        }
    }
}
