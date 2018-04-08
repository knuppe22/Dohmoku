using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dohmoku
{
    class Heuristics
    {
        public static int maxValue = 2000000000;
        public static int minValue = -2000000000;

        // Evaluation function. TODO: fill this method
        public static int Calculate(int[,] board)
        {
            if (Game.IsEnded(board, Game.player.Opposite()))
            {
                return maxValue;
            }
            else if (Game.IsEnded(board, Game.player))
            {
                return minValue;
            }

            int result = 0;
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                int sum = 0;
                int flag = team == Game.player ? -1 : 1;
                // TODO
                // 4x3
                // 3x3
                if (OpenedFourInARow(board, team) > 0)
                {
                    sum += 75 * (int)Math.Pow(OpenedFourInARow(board, team), 2);
                }
                else if (HalfClosedFourInARow(board, team) > 0)
                {
                    sum += 25 * (int)Math.Pow(HalfClosedFourInARow(board, team), 2);
                }
                result += sum * flag;
            }
            return result;
        }
        static int ThreeThree(int[,] board, Team team)
        {
            int count = 0;

            return count;
        }
        static int OpenedFourInARow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (!(!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite()) &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team &&
                        !(!Game.OnBoard(i, j + 5) || board[i, j + 5] == (int)team.Opposite()))
                    {
                        count++;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (!(!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite()) &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team &&
                        !(!Game.OnBoard(i + 5, j) || board[i + 5, j] == (int)team.Opposite()))
                    {
                        count++;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check \ form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (!(!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite()) &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team &&
                        !(!Game.OnBoard(i + 5, j + 5) || board[i + 5, j + 5] == (int)team.Opposite()))
                    {
                        count++;
                    }
                }
            }
            for (int i = 2; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 16; j++)
                {
                    if (!(!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite()) &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == (int)team &&
                        !(!Game.OnBoard(i - 5, j + 5) || board[i - 5, j + 5] == (int)team.Opposite()))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        static int HalfClosedFourInARow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i, j + 5) || board[i, j + 5] == (int)team.Opposite())) &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team
                        )
                    {
                        count++;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i + 5, j) || board[i + 5, j] == (int)team.Opposite())) &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team)
                    {
                        count++;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check \ form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i + 5, j + 5) || board[i + 5, j + 5] == (int)team.Opposite())) &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                }
            }
            for (int i = 2; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 16; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i - 5, j + 5) || board[i - 5, j + 5] == (int)team.Opposite())) &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        bool FullyClosedFourInARow(int[,] board, Team team)
        {
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i, j + 5) || board[i, j + 5] == (int)team.Opposite())) &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team
                        )
                    {
                        return true;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i + 5, j) || board[i + 5, j] == (int)team.Opposite())) &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = -1; i < 15; i++)    // Check \ form
            {
                for (int j = -1; j < 15; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i + 5, j + 5) || board[i + 5, j + 5] == (int)team.Opposite())) &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 2; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 16; j++)
                {
                    if (((!Game.OnBoard(i, j) || board[i, j] == (int)team.Opposite())
                            ^ (!Game.OnBoard(i - 5, j + 5) || board[i - 5, j + 5] == (int)team.Opposite())) &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == (int)team)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool FourInARow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == (int)team)
                    {
                        count++;
                        if (count >= 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                count = 0;
            }
            for (int i = 0; i < 19; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[j, i] == (int)team)
                    {
                        count++;
                        if (count >= 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                count = 0;
            }
            for (int i = 0; i < 16; i++)    // Check \ form
            {
                for (int j = 0; j < 16; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 2; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 16; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool ThreeInARow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == (int)team)
                    {
                        count++;
                        if (count >= 3)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                count = 0;
            }
            for (int i = 0; i < 19; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[j, i] == (int)team)
                    {
                        count++;
                        if (count >= 3)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                count = 0;
            }
            for (int i = 0; i < 17; i++)    // Check \ form
            {
                for (int j = 0; j < 17; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 2; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 17; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
