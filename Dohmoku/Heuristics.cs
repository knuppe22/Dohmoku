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
                if (OpenedFourInRow(board, team) > 0)
                {
                    sum += 125 * (int)Math.Pow(OpenedFourInRow(board, team), 2);
                }
                else if (HalfClosedFourInRow(board, team) > 0)
                {
                    sum += 50 * (int)Math.Pow(HalfClosedFourInRow(board, team), 2);
                }
                else if (SeparatedFourInRow(board, team) > 0)
                {
                    sum += 70 * (int)Math.Pow(SeparatedFourInRow(board, team), 2);
                }
                else if (OpenedThreeInRow(board, team) > 0)
                {
                    sum += 40 * (int)Math.Pow(OpenedThreeInRow(board, team), 2);
                }
                // SeparatedThree
                result += sum * flag;
            }
            return result;
        }
        static int ThreeThree(int[,] board, Team team)
        {
            int count = 0;

            return count;
        }
        static int OpenedFourInRow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 14; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team &&
                        board[i, j + 5] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 14; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team &&
                        board[i + 5, j] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 14; i++)    // Check \ form
            {
                for (int j = 0; j < 14; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team &&
                        board[i + 5, j + 5] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 5; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 14; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == (int)team &&
                        board[i - 5, j + 5] == 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        static int ClosedFourInRow(int[,] board, Team team)
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
        static int HalfClosedFourInRow(int[,] board, Team team)
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
            for (int i = 4; i < 19; i++)    // Check / form
            {
                for (int j = -1; j < 15; j++)
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
        static int SeparatedFourInRow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i, j + 1] == 0 &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == 0 &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == 0 &&
                        board[i, j + 4] == (int)team)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j] == 0 &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == 0 &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == 0 &&
                        board[i + 4, j] == (int)team)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check \ form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j + 1] == 0 &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                       board[i + 1, j + 1] == (int)team &&
                       board[i + 2, j + 2] == 0 &&
                       board[i + 3, j + 3] == (int)team &&
                       board[i + 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                       board[i + 1, j + 1] == (int)team &&
                       board[i + 2, j + 2] == (int)team &&
                       board[i + 3, j + 3] == 0 &&
                       board[i + 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                }
            }
            for (int i = 4; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i - 1, j + 1] == 0 &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                       board[i - 1, j + 1] == (int)team &&
                       board[i - 2, j + 2] == 0 &&
                       board[i - 3, j + 3] == (int)team &&
                       board[i - 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                    else if (board[i, j] == (int)team &&
                       board[i - 1, j + 1] == (int)team &&
                       board[i - 2, j + 2] == (int)team &&
                       board[i - 3, j + 3] == 0 &&
                       board[i - 4, j + 4] == (int)team)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        bool FullyClosedFourInRow(int[,] board, Team team)
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
        static int OpenedThreeInRow(int[,] board, Team team)
        {
            int count = 0;
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check \ form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == 0)
                    {
                        count++;
                    }
                }
            }
            for (int i = 4; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == 0 &&
                        board[i - 1, j + 1] == (int)team &&
                        board[i - 2, j + 2] == (int)team &&
                        board[i - 3, j + 3] == (int)team &&
                        board[i - 4, j + 4] == 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
