using System;
using System.Collections.Generic;

namespace Dohmoku
{
    class AI
    {
        int maxDepth = 3;      // How deep AI can see?
        int maxValue = 2000000000;
        int minValue = -2000000000;

        public int[] Think()		// Return AI's result coordination with an input of current board. TODO: make this method
        {
            Tree root = new Tree(Game.player.Opposite(), -1, -1);
            MakeTree(root, 0, Game.board);
            root.value = AlphaBeta(root, 0, minValue, maxValue);

            for (int i = 0; i < root.children.Count; i++)
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

        void MakeTree(Tree tree, int depth, int[,] state)
        {
            if (depth < maxDepth)
            {
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        if (Game.IsPlacable(state, i, j) && IsAdjacent(state, i, j))
                        {
                            Tree newTree = new Tree(tree.team.Opposite(), i, j);
                            int[,] newState = new int[19, 19];

                            Array.Copy(state, newState, state.Length);
                            newState[i, j] = (int)(tree.team);

                            tree.AddChild(newTree);
                            MakeTree(newTree, depth + 1, newState);
                        }
                    }
                }
            }
            else if (depth == maxDepth)
            {
                tree.value = Calculate(state);
            }
        }

        int AlphaBeta(Tree tree, int depth, int alpha, int beta)   // TODO: Do this with DFS
        {
            if (depth == maxDepth)
            {
                return tree.value;
            }
            if (tree.team == Game.player)   // Minimizer
            {
                int value = maxValue;
                for (int i = 0; i < tree.children.Count; i++)
                {
                    tree.children[i].value = AlphaBeta(tree.children[i], depth + 1, alpha, beta);
                    if (value > tree.children[i].value)
                    {
                        value = tree.children[i].value;
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
                int value = minValue;
                for(int i = 0; i < tree.children.Count; i++)
                {
                    tree.children[i].value = AlphaBeta(tree.children[i], depth + 1, alpha, beta);
                    if (value < tree.children[i].value)
                    {
                        value = tree.children[i].value;
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
            }
            return result;
        }
    }

    class Tree
    {
        public List<Tree> children = new List<Tree>();

        public Team team;       // Who's turn at the value state?
        public int[] xy = new int[2];   // Different cell
        public int value;

        public Tree(Team team, int x, int y)
        {
            this.team = team;
            xy[0] = x;
            xy[1] = y;
        }

        public void AddChild(Tree child)
        {
            children.Add(child);
        }
    }
}
