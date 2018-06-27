using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CommonInterviewQuestionsPractice
{
    class OtherProblems
    {
        private static OtherProblems instance;

        public static OtherProblems GetInstance()
        {
            if(instance == null)
            {
                instance = new OtherProblems();
            }

            return instance;
        }

        public void RunProblems()
        {
            Console.WriteLine("===========================Other Problems=============================");

            int[][] matrix = new int[][]
            {
                new int[]{1, 5, 7 },
                new int[]{8, 6, 4 },
                new int[]{2, 3, 9 },
            };

            List<Pair> path = GetLongestPath(1, 0, matrix);

            for(int i = 0; i < path.Count; i++)
            {
                Console.Write(matrix[path[i].num1][path[i].num2] + " -> ");
            }

            LongestUniqueSubstring("abcdab");
        }

        /// <summary>
        /// Returns an array containing index pairs of the longest path possible from a starting index (x,y) in an MxN matrix
        /// where the current position can only move left, right, up or down to an index containin a smaller number.
        /// 
        /// Searches down, left, up, then right.
        /// 
        /// O((mxn)^2) time with O(n) space
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>

        List<Pair> pathTracker = new List<Pair>();
        private List<Pair> GetLongestPath(int x, int y, int[][] matrix)
        {
            List<Pair> path = new List<Pair>() {new Pair(x, y)};

            //check below
            if (x+1 < matrix.Length && matrix[x][y] > matrix[x + 1][y]) { 
                path.AddRange(GetLongestPath(x + 1, y, matrix));
            }
            //check left
            if (y-1 >= 0 && matrix[x][y] > matrix[x][y-1])
            {
                path.AddRange(GetLongestPath(x, y - 1, matrix));
            }
            //check up
            if (x-1 >= 0 && matrix[x][y] > matrix[x - 1][y])
            {
                path.AddRange(GetLongestPath(x - 1, y, matrix));
            }
            //check right
            if (y+1 < matrix[x].Length && matrix[x][y] > matrix[x][y + 1])
            {
                path.AddRange(GetLongestPath(x, y+1, matrix));
            }

            return path;
        }

        class Pair
        {
            public int num1, num2;

            public Pair(int num1, int num2)
            {
                this.num1 = num1;
                this.num2 = num2;
            }
        }

        private void LongestUniqueSubstring(string str)
        {
            Hashtable hashmap = new Hashtable();

            string currentString = "";
            string biggestSoFar = "";

            for(int i = 0; i < str.Length; i++)
            {
                if(!hashmap.ContainsKey(str[i] - 0))
                {
                    hashmap.Add(str[i] - 0, str[i]);
                    currentString = currentString + str[i];
                }
                else
                {
                    if (currentString.Length > biggestSoFar.Length)
                    {
                        biggestSoFar = currentString;
                    }
                    currentString = "";
                    hashmap.Clear();
                }
            }

            Console.WriteLine('\n' + biggestSoFar);
        }
    }
}
