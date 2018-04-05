using System;
using System.Collections.Generic;

namespace Dohmoku
{
    class AI
    {
        int maxDepth = 4;      // How deep AI can see?

        public int[] Think()		// Return AI's result coordination with an input of current board. TODO: make this method
        {
            Tree tree = new Tree(Game.player.Opposite(), Game.board);
            MakeTree(tree, 0);

            int[] result;
            do
            {
                result = Random();
            } while (!Game.IsPlacable(Game.board, result));

            return result;
        }

        int[] Random()				// Return 2 random integers from 0 to 18. For sample.
        {
            Random r = new Random();
            int x = r.Next(0, 19);
            int y = r.Next(0, 19);

            return new int[2] { x, y };
        }

        void MakeTree(Tree root, int depth)
        {
            if (depth < maxDepth)
            {
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        if (Game.IsPlacable(root.value, i, j))
                        {
                            int[,] newBoard = new int[19, 19];
                            Array.Copy(root.value, newBoard, root.value.Length);
                            newBoard[i, j] = (int)(root.team.Opposite());
                            root.AddChild(new Tree(root.team.Opposite(), newBoard));
                        }
                    }
                }
                for (int i = 0; i < root.children.Count; i++)
                {
                    MakeTree(root.children[i], depth + 1);
                }
            }
        }

        double AlphaBeta(Tree tree, int depth, double alpha, double beta, bool maximizing)
        {
            return 0;   // TODO
        }

        // Evaluation function. TODO: fill this method
        double Calculate(int[,] board)
        {
            return 0;
        }
    }

    class Tree
    {
        public Tree parent = null;
        public List<Tree> children = new List<Tree>();

        public Team team;       // Who's turn at the value state?
        public int[,] value = new int[19, 19];
        public double heuristic;

        public double alpha;
        public double beta;

        public Tree(Team team, int[,] board)
        {
            this.team = team;
            Array.Copy(board, value, board.Length);
        }

        public void AddChild(Tree child)
        {
            children.Add(child);
            child.parent = this;
        }
    }
}
