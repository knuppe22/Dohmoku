using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dohmoku
{
    class AI
    {
        int maxDepth = 4;       // How deep AI can see?
        int curDepth = 1;
        long timeLimit = 10000; // Time limit
        Stopwatch stopwatch;

        public int[] Think()		// Return AI's result coordination with an input of current board.
        {
            stopwatch = Stopwatch.StartNew();

            int[] result = null;
            for (curDepth = 2; curDepth <= maxDepth; curDepth++)
            {
                Tree root = new Tree(Game.player.Opposite(), -1, -1);
                MakeTree(root, 0, Game.board);
                root.value = AlphaBeta(root, 0, Heuristics.minValue, Heuristics.maxValue);
                if (stopwatch.ElapsedMilliseconds >= timeLimit)
                {
                    break;
                }

                for (int i = 0; i < root.children.Count; i++)
                {
                    if (root.children[i].value == root.value)
                    {
                        result = root.children[i].xy;
                    }
                }
            }
            if (result == null)
            {
                do
                {
                    result = Random();
                } while (!Game.IsPlacable(Game.board, result));
            }
            return result;
        }

        int[] Random()				// Return 2 random integers from 0 to 18.
        {
            Random r = new Random();
            int x = r.Next(0, 19);
            int y = r.Next(0, 19);

            return new int[2] { x, y };
        }

        void MakeTree(Tree tree, int depth, int[,] state)
        {
            if (stopwatch.ElapsedMilliseconds >= timeLimit)
            {
                return;
            }
            if (depth < curDepth)
            {
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        if (IsAdjacent(state, i, j) && Game.IsPlacable(state, i, j))
                        {
                            Tree newTree = new Tree(tree.team.Opposite(), i, j);
                            int[,] newState = new int[19, 19];

                            Array.Copy(state, newState, state.Length);
                            newState[i, j] = (int)(tree.team);

                            tree.AddChild(newTree);
                            if (Game.IsEnded(newState, tree.team))
                            {
                                tree.value = Heuristics.Calculate(newState);
                            }
                            else
                            {
                                MakeTree(newTree, depth + 1, newState);
                            }
                        }
                    }
                }
            }
            else if (depth == curDepth)
            {
                tree.value = Heuristics.Calculate(state);
            }
        }

        int AlphaBeta(Tree tree, int depth, int alpha, int beta)    // Mini-max & Alpha beta pruning
        {
            if (stopwatch.ElapsedMilliseconds >= timeLimit)
            {
                return int.MinValue;
            }
            if (tree.children.Count == 0)   // If leaf node
            {
                return tree.value;
            }
            if (tree.team == Game.player)   // Minimizer
            {
                int value = Heuristics.maxValue;
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
                int value = Heuristics.minValue;
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
        
        public static bool IsAdjacent(int[,] board, int x, int y)   // Is near to placed stones?
        {
            for (int i = x - 1; i < x + 2; i++)
            {
                if (i < 0 || i > 18)
                {
                    continue;
                }
                for (int j = y - 1; j < y + 2; j++)
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
    }

    class Tree
    {
        public List<Tree> children = new List<Tree>();

        public Team team;       // Who's turn at this state?
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
