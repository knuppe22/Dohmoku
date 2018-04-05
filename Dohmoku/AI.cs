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
        int depth = 4;
		
        public AI(Team playerTeam)
        {
            team = playerTeam.Opposite();
        }

        public int[] Think()		// Return AI's result coordination with an input of current board. TODO: make this method
        {
            Tree tree = new Tree(Game.board);


            for (int i = 0; i < depth; i++)
            {
                
            }

            int[] result;
            do
            {
                result = Random();
            } while (!Game.IsPlacable(result));

            return result;
        }

        int[] Random()				// Return 2 random integers from 0 to 18. For sample.
        {
            Random r = new Random();
            int x = r.Next(0, 19);
            int y = r.Next(0, 19);

            return new int[2] { x, y };
        }

        double Calculate(int[,] board)  // Evaluation function. TODO: fill this method
        {
            return 0;
        }

        double AlphaBeta(Tree tree, int depth , double alpha, double beta, bool maximizing)
        {
            return 0;   // TODO
        }
    }

    class Tree
    {
        // TODO: Make tree addchild deletechild

        Tree parent;
        List<Tree> children;

        int[,] value;
        double heuristic;

        double alpha;
        double beta;
        bool maximizer;

        public Tree(int [,] board)
        {
            value = board;
        }
    }
}
