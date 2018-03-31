using System;

namespace Dohmoku
{
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game();
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
						game.player = Team.Black;
						isTeamSelected = true;
						break;
					case "W":
					case "w":
						game.player = Team.White;
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
			game.Start();
		}
	}
}
