using System;

namespace Dohmoku
{
	public enum Team { Black = 1, White }
	public static class MethodsForTeam
	{
		public static Team Opposite(this Team team)
		{
			return (Team)(3 - (int)team);
		}
	}

	class Game
	{
		public static Team player;					        // Current player
		public static int[,] board = new int[19, 19];		// Gomoku board. Location is [column#, row#]. 0: empty, 1: black, 2: white
		Team current = Team.Black;			// Current playing player
		Team? winner = null;				// Winner of the game. Initialized by null.
        AI ai;

        public Game(Team playerTeam)
        {
            player = playerTeam;
            ai = new AI();
        }

		public void Start()
		{
			Console.Clear();
			Console.WriteLine("Start!");

			while (winner == null)
			{
				DrawBoard();
                if (current == player)		// If player's turn
                {
                    Console.WriteLine(current + " team's turn. Enter location to place stone with the format of \"xy\", e.g., \"A1\".");
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
                    else if (!IsPlacable(board, x, y))
                    {
                        Console.WriteLine("You cannot place here! Enter again.");
                        continue;
                    }
                    Place(current, x, y);
                }
                else	// If AI's turn
                {
                    Console.WriteLine(current + " team(AI)'s turn.");
                    int[] result = ai.Think();
                    Console.WriteLine("AI placed stone in " + (char)(result[0] + 'A') + (result[1] + 1) + ".");
                    Place(current, result);
                }

                if (IsEnded(board, current))
                {
                    winner = current;
                }
                else
                {
                    current = current.Opposite();
                }
			}
            
			DrawBoard();
			Console.WriteLine(winner.Value + " team wins!");
			Console.WriteLine(winner == player ? "You win!" : "You lose...");
		}
		
		void DrawBoard()		// Draw gomoku board
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

        int[] Parser(string s)  // Return { column#, row# } with the input string of player
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

        public static bool OnBoard(int x, int y)
        {
            return x >= 0 && x < 19 && y >= 0 && y < 19;
        }
		public static bool IsPlacable(int[,] board, int x, int y)	// Is board[x, y] is empty?
		{
			return OnBoard(x, y) && board[x, y] == 0;
		}
		public static bool IsPlacable(int[,] board, int[] xy)
		{
			if (xy.Length != 2)
			{
				throw new Exception("Invalid int[] input");
			}
			else
			{
				return IsPlacable(board, xy[0], xy[1]);
			}
		}

		void Place(Team team, int x, int y)			// Place team's stone in board[x, y]
		{
			if (IsPlacable(board, x, y))
			{
				board[x, y] = (int)team;
			}
			else
			{
				throw new Exception("Can't place here");
			}
		}
        void Place(Team team, int[] xy)
        {
            if (xy.Length != 2)
            {
                throw new Exception("Invalid int[] input");
            }
            else
            {
                Place(team, xy[0], xy[1]);
            }
        }

        public static bool IsEnded(int [,] board, Team team)
        {
            for (int i = 0; i < 19; i++)    // Check | form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i, j + 1] == (int)team &&
                        board[i, j + 2] == (int)team &&
                        board[i, j + 3] == (int)team &&
                        board[i, j + 4] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check - form
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j] == (int)team &&
                        board[i + 2, j] == (int)team &&
                        board[i + 3, j] == (int)team &&
                        board[i + 4, j] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < 15; i++)    // Check \ form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
                        board[i + 1, j + 1] == (int)team &&
                        board[i + 2, j + 2] == (int)team &&
                        board[i + 3, j + 3] == (int)team &&
                        board[i + 4, j + 4] == (int)team)
                    {
                        return true;
                    }
                }
            }
            for (int i = 4; i < 19; i++)    // Check / form
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)team &&
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
	}
}
