using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dohmoku
{
	public enum Team { Black, White }

	public class Game
	{
		public Team player;					// Current player
		int[,] board = new int[19, 19];		// Gomoku board. Location is [column#, row#]. 0: empty, 1: black, 2: white
		Team current = Team.Black;			// Current playing player
		Team? winner = null;				// Winner of the game. Initialized by null.

		public void Start()
		{
			Console.Clear();
			Console.WriteLine("Start!");

			while (winner == null)
			{
				DrawBoard();
				Console.WriteLine(current.ToString("g") + " team's turn. Enter location to place stone with the format of \"xy\", e.g., \"A1\".");
				int[] location = Parser(Console.ReadLine());
                if (location == null)
                {
                    Console.WriteLine("Wrong format! Enter again.");
                    continue;
                }
                int x = location[0];
                int y = location[1];
				if (y > 18)
				{
					Console.WriteLine("Out of index! Enter again.");
					continue;
				}
				else if (!IsPlacable(x, y))
				{
					Console.WriteLine("You cannot place here! Enter again.");
					continue;
				}
				Place(current, x, y);

                if (IsEnded(current))
                {
                    winner = current;
                }
                else
                {
                    current = (Team)(((int)current + 1) % 2);
                }
			}
            
			DrawBoard();
			Console.WriteLine(winner.Value.ToString("g") + " team wins!");
		}
		
		void DrawBoard()		// Method drawing gomoku board
		{
			Console.Write("yx");
			for (int i = 0; i < 19; i++)
			{
				Console.Write(" " + (char)('A' + i));
			}
			Console.WriteLine();

			for (int i = 0; i < 19; i++)
			{
				Console.Write((i + 1).ToString("D2"));
				for (int j = 0; j < 19; j++)
				{
					Console.Write(Int2Char(board[j, i]));
				}

				Console.WriteLine();
			}
		}
		string Int2Char(int n)	// For DrawBoard method
		{
			switch (n)
			{
				case 0:
					return "┼ ";
				case 1:
					return "●";
				case 2:
					return "○";
				default:
					throw new Exception("Invalid data is stored");
			}
		}

        int[] Parser(string s)  // Return { column#, row# } with the input of read string
        {
            int flag = 0;
            int x = 0;
            int y = 0;
            bool enter = false;
            foreach(char c in s)
            {
                switch (flag)
                {
                    case 0:
                        if (c >= 'a' && c <= 's')
                        {
                            x = c - 'a';
                            flag++;
                        }
                        else if (c >= 'A' && c <= 'S')
                        {
                            x = c - 'A';
                            flag++;
                        }
                        break;
                    case 1:
                        if (c >= '0' && c <= '9')
                        {
                            enter = true;
                            y = y * 10 + (c - '0');
                        }
                        else if (enter)
                        {
                            flag++;
                        }
                        break;
                }
            }
            if (flag >= 1)
            {
                return new int[2] { x, y - 1 };
            }
            return null;
        }

		bool IsPlacable(int x, int y)	// Is board[x, y] is zero?
		{
			return board[x, y] == 0;
		}
		void Place(Team team, int x, int y)	// Place team's stone in board[x, y]
		{
			if (IsPlacable(x, y))
			{
				board[x, y] = (int)team + 1;
			}
			else
			{
				throw new Exception("Can't place here");
			}
		}

        bool IsEnded(Team team)
        {
            return false;
        }
	}
}
