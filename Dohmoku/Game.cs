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
		int[,] board = new int[19, 19];		// Gomoku board, 0: empty, 1: black, 2: white
		Team current = Team.Black;			// Current playing player
		Team? winner = null;				// Winner of the game. Initialized by null.

		public void Start()
		{
			Console.Clear();
			Console.WriteLine("Start!");

			while (winner == null)
			{
				DrawBoard();
				Console.WriteLine(current.ToString("g") + " team's turn. Enter coordinate to place stone with the format of \"i j\"."); // TODO: Change to j 10, make exception handling
				string str = Console.ReadLine();
				string[] val = str.Split(' ');
				int x = int.Parse(val[0]);
				int y = int.Parse(val[1]);
				if (x < 0 || x > 18 || y < 0 || y > 18)
				{
					Console.WriteLine("Out of index! Enter again.");
					continue;
				}
				if (!IsPlacable(x, y))
				{
					Console.WriteLine("You cannot place here! Enter again.");
					continue;
				}
				Place(current, x, y);
				current = (Team)(((int)current + 1) % 2);
			}
		}
		
		void DrawBoard()
		{
			Console.Write("ij");
			for (int i = 0; i < 19; i++)
			{
				Console.Write(i.ToString("D2"));
			}
			Console.WriteLine();

			for (int i = 0; i < 19; i++)
			{
				Console.Write(i.ToString("D2"));
				for (int j = 0; j < 19; j++)
				{
					Console.Write(Int2Char(board[i, j]));
				}

				Console.WriteLine();
			}
		}

		string Int2Char(int n)
		{
			switch(n)
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

		bool IsPlacable(int x, int y)
		{
			return board[x, y] == 0;
		}
		void Place(Team team, int x, int y)
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
	}
}
