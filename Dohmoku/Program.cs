using System;

namespace Dohmoku
{
	class Program
	{
		static void Main(string[] args)
		{
            Game game = null;
			bool isTeamSelected = false;
			
			while(!isTeamSelected)
			{
				Console.WriteLine("Choose team. (B/W)");
				Console.WriteLine("Q to exit.");
				string str = Console.ReadLine();
				switch (str)
				{
					case "B":
					case "b":
                        game = new Game(Team.Black);
						isTeamSelected = true;
						break;
					case "W":
					case "w":
                        game = new Game(Team.White);
						isTeamSelected = true;
						break;
					case "Q":
					case "q":
						Console.WriteLine("Bye");
						return;
					default:
						Console.WriteLine("Invalid input");
						break;
				}
			}
            if (game != null)
            {
                game.Start();
            }
		}
	}
}
