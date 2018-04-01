using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dohmoku
{
    class AI
    {
        public Team team;

        public AI(Team playerTeam)
        {
            team = (Team)(((int)playerTeam + 1) % 2);
        }

        public int[] Think(int[,] board)       // Return AI's result coordination with an input of current board. TODO: make this method
        {
            int[] result;
            do
            {
                result = Random();
            } while (board[result[0], result[1]] != 0);

            return result;
        }

        int[] Random()      // Return 2 random integers from 0 to 18. For sample.
        {
            Random r = new Random();
            int x = r.Next(0, 19);
            int y = r.Next(0, 19);

            return new int[2] { x, y };
        }
    }
}
