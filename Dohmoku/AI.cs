using System;
using System.Collections.Generic;

namespace Dohmoku
{
    class AI
    {
        int maxDepth = 3;      // How deep AI can see?

        public int[] Think()		// Return AI's result coordination with an input of current board. TODO: make this method
        {
            Tree root = new Tree(Game.player.Opposite(), -1, -1, Game.board);
            MakeTree(root, 0);
            DFS(root, 0);
            for(int i = 0; i < root.children.Count; i++)
            {
                if (root.children[i].value == root.value)
                {
                    return root.children[i].xy;
                }
            }
            throw new Exception("Something wrong");

            /*
            int[] result;
            do
            {
                result = Random();
            } while (!Game.IsPlacable(Game.board, result));

            return result;
            */
        }

        int[] Random()				// Return 2 random integers from 0 to 18. For sample.
        {
            Random r = new Random();
            int x = r.Next(0, 19);
            int y = r.Next(0, 19);

            return new int[2] { x, y };
        }

        void MakeTree(Tree tree, int depth)
        {
            if (depth < maxDepth)
            {
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        if (Game.IsPlacable(tree.state, i, j) && IsAdjacent(tree.state, i, j))
                        {
                            int[,] newBoard = new int[19, 19];
                            Array.Copy(tree.state, newBoard, tree.state.Length);
                            newBoard[i, j] = (int)(tree.team);

                            tree.AddChild(new Tree(tree.team.Opposite(), i, j, newBoard));
                        }
                    }
                }
                for (int i = 0; i < tree.children.Count; i++)
                {
                    MakeTree(tree.children[i], depth + 1);
                }
            }
        }

        void DFS(Tree tree, int depth)
        {
            for(int i = 0; i < tree.children.Count; i++)
            {
                DFS(tree.children[i], depth + 1);
            }

            tree.value = AlphaBeta(tree, depth, -2000000000, 2000000000);
        }

        int AlphaBeta(Tree tree, int depth, int alpha, int beta)   // TODO: Do this with DFS
        {
            if (depth == maxDepth)
            {
                return Calculate(tree.state);
            }
            if (tree.team == Game.player)   // Minimizer
            {
                int value = 2000000000;
                for (int i = 0; i < tree.children.Count; i++)
                {
                    int tmp = AlphaBeta(tree.children[i], depth + 1, alpha, beta);
                    if (value > tmp)
                    {
                        value = tmp;
                    }
                    if (beta > value)
                    {
                        beta = value;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return value;
            }
            else                            // Maximizer
            {
                int value = -2000000000;
                for(int i = 0; i < tree.children.Count; i++)
                {
                    int tmp = AlphaBeta(tree.children[i], depth + 1, alpha, beta);
                    if (value < tmp)
                    {
                        value = tmp;
                    }
                    if (alpha < value)
                    {
                        alpha = value;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return value;
            }
        }
        
        public static bool IsAdjacent(int[,] board, int x, int y)
        {
            for (int i = x - 2; i < x + 3; i++)
            {
                if (i < 0 || i > 18)
                {
                    continue;
                }
                for (int j = y - 2; j < y + 3; j++)
                {
                    if (j < 0 || j > 18)
                    {
                        continue;
                    }
                    if (board[i, j] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Evaluation function. TODO: fill this method
        int Calculate(int[,] board)
        {
            if (Game.IsEnded(board, Game.player))
            {
                return -2000000000;
            }
            if (Game.IsEnded(board, Game.player.Opposite()))
            {
                return 2000000000;
            }
            return 0;
        }
    }

    class Tree
    {
        public Tree parent = null;
        public List<Tree> children = new List<Tree>();

        public Team team;       // Who's turn at the value state?
        public int[] xy = new int[2];
        public int[,] state = new int[19, 19];
        public int value;

        public Tree(Team team, int x, int y, int[,] board)
        {
            this.team = team;
            xy[0] = x;
            xy[1] = y;
            state = board;
        }

        public void AddChild(Tree child)
        {
            children.Add(child);
            child.parent = this;
        }
    }
}
