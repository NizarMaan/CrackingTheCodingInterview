using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CommonInterviewQuestionsPractice
{
    class Chapter1Problems
    {
        private static Chapter1Problems instance = null;

        public static Chapter1Problems GetInstance()
        {
            if(instance == null)
            {
                instance = new Chapter1Problems();
            }

            return instance;
        }

        public void RunProblems()
        {
            Console.WriteLine("===========================Chapter 1=============================");
            Console.WriteLine(StringWithUniqueChars("test")); //false
            Console.WriteLine(StringWithUniqueChars("abcd")); //true
            Console.WriteLine(IsPermutation("test", "ttse")); //true
            Console.WriteLine(IsPermutation("test", "tese")); //false

            int a = 1;
            int b = 2;
            Console.WriteLine(a + " " + b);
            Swap(a, b); //testing if pass by reference or value -- it is pass by value...
            Console.WriteLine(a + " " + b);

            Console.WriteLine(URLify("I like to program things                                                            ", 24));

            int[] testList = new int[] { 2, 7, -2, 0, 2, 1, 3, 11, 5, 4, 6, 12, 10 };
            Console.WriteLine(HasPair(testList, 8)); //pair {7, 1}
            Console.WriteLine(HasPair(testList, -2)); //pair {-2, 0}
            Console.WriteLine(HasPair(testList, 24)); //no pair

            string[] palindromeTest = new string[] { "cvcii", "Anna", "akyak", "tcao cta", "racecar", "abcdefg", "mom", "noon" };
            //true   //false  //true  //true     //true      //false   //true  //true
            foreach (string str in palindromeTest)
            {
                Console.WriteLine(str + ",  " + IsPalindromePermutation(str));
            }

            Console.WriteLine(OneEditAway("pale", "ple")); //true
            Console.WriteLine(OneEditAway("pales", "pale")); //true
            Console.WriteLine(OneEditAway("pale", "bale")); //true
            Console.WriteLine(OneEditAway("pale", "bae")); //false
            Console.WriteLine(OneEditAway("pale", "paleee")); //false
            Console.WriteLine(OneEditAway("pale", "pale")); //false

            Console.WriteLine(CompressString("aaaaaaaaaabc")); //a10b1c1
            Console.WriteLine(CompressString("aaaaaaaaaabbbbbbbbbbbbbbbccccccccccddddddddddddd")); //a10b15c10d13
            Console.WriteLine(CompressString("aaaaaaaaaabcdefghijklmnopqrstuvwxyz")); //return original string

            int[][] matrix = new int[][]
            {
                new int[]{1, 1, 4, 5},
                new int[]{8, 5, 3, 6},
                new int[]{9, 8, 2, 5},
                new int[]{3, 2, 7, 9}
            };

            PrintMatrix(RotateMatrix(matrix));

            matrix = new int[][]
            {
                new int[]{1, 2, 3, 4, 5},
                new int[]{1, 2, 3, 4, 5},
                new int[]{1, 2, 3, 4, 5},
                new int[]{1, 2, 3, 4, 5},
                new int[]{1, 2, 3, 4, 5}
            };

            PrintMatrix(RotateMatrix(matrix));

            matrix = new int[][]
            {
                new int[]{1, 0, 3, 5},
                new int[]{2, 0, 1, 0},
                new int[]{6, 8, 9, 4}
            };

            PrintMatrix(MatrixZerofy(matrix));

            Console.WriteLine(IsRotation("hello", "lohel"));
        }

        /*----------------------pg. 90 Question 1.1-------------------------------------------*/
        public  bool StringWithUniqueChars(string str)
        {
            Hashtable map = new Hashtable();

            for (int i = 0; i < str.Length; i++)
            {
                if (map[CharToInt(str[i])] != null)
                {
                    return false;
                }

                map.Add(CharToInt(str[i]), str[i]);
            }

            return true;
        }

        private int CharToInt(char c)
        {
            return c - '0';
        }
        /*------------------------------------------------------------------------------------*/

        //testing if pass by reference or value
        private void Swap(int a, int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /*----------------------pg. 90 Question 1.2-------------------------------------------*/
        //TODO: also check each char count. 
        private bool IsPermutation(string str1, string str2)
        {
            if (str1.Length != str2.Length)
            {
                return false;
            }

            int[] charTracker = new int[128]; //assuming ASCII character codes, 128 of them

            //count each character in str1
            for (int i = 0; i < str1.Length; i++)
            {
                charTracker[str1[i]]++;
            }

            //for each char in str2 decrease the corresponding index that holds the counter in charTracker
            for (int i = 0; i < str2.Length; i++)
            {
                charTracker[str2[i]]--;

                /*if the value at the current character's (str2[i]) index (charTracker[str2[i]]) is less than 0
                    then str2 has a count mismatch for the char in question, hence, it is not a permutation.
                 */
                if (charTracker[str2[i]] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 90 Question 1.3-------------------------------------------*/
        private string URLify(string str, int trueLength) //param: trueLength is the length of the string ignoring empty space after the last non-empty char
        {
            int spacesCount = 0;
            char[] specialChar = new char[] { '%', '2', '0' };
            StringBuilder newString = new StringBuilder
            {
                Length = str.Length
            };

            //count spaces
            for (int i = 0; i < trueLength; i++)
            {
                if (Char.IsWhiteSpace(str[i]))
                {
                    spacesCount++;
                }
            }

            int newTrueLength = trueLength + (2 * spacesCount);

            //shift characters down
            for (int i = trueLength - 1; i >= 0; i--)
            {
                if (spacesCount > 0)
                {
                    if (Char.IsWhiteSpace(str[i]))
                    {
                        spacesCount--;
                    }
                    else
                    {
                        newString[i + (2 * spacesCount)] = str[i];
                    }
                }
                else
                {
                    newString[i] = str[i];
                }
            }

            //replace spaces with %20 characters
            int counter = 0;
            for (int i = 0; i < newTrueLength; i++)
            {
                if (counter == specialChar.Length)
                {
                    counter = 0;
                }
                else
                {
                    if (newString[i] == 0)
                    {
                        newString[i] = specialChar[counter];
                        counter++;
                    }
                }
            }

            return newString.ToString();
        }
        /*------------------------------------------------------------------------------------*/

        //Google "finding pairs in a list that add up to a sum"
        private string HasPair(int[] list, int desiredSum)
        {
            Hashtable numTracker = new Hashtable();
            for (int i = 0; i < list.Length; i++)
            {
                if (numTracker[desiredSum - list[i]] != null)
                {
                    return new Pair<int, int>(desiredSum - list[i], list[i]).ToString();
                }
                //if number not already in our hashtable
                if (numTracker[list[i]] == null)
                {
                    numTracker.Add(list[i], list[i]);
                }
            }

            return "no pair";
        }

        class Pair<T, Y>
        {

            public T Item1 { get; set; }

            public Y Item2 { get; set; }

            public Pair(T item1, Y item2)
            {
                Item1 = item1;
                Item2 = item2;
            }

            override
            public string ToString()
            {
                return "Pair: {" + Item1 + ", " + Item2 + "}";
            }
        }

        /*----------------------pg. 91 Question 1.4-------------------------------------------*/

        /*an even length palindrome must have an even amount of each letter, an odd lengthed palindrome
        has at least (typically the middle character) one letter which does not appear an even amount of times
        so we can count the amount of characters which appear an odd amount of times. If there is more than 1 char
        that appears an odd amount of times then the string is not a permutation of a palindrome
        */
        private bool IsPalindromePermutation(string str)
        {
            int[] charTracker = new int[128]; //assuming ASCII character set values 0-128

            //count each char
            for (int i = 0; i < str.Length; i++)
            {
                //ignore space counts
                if (!Char.IsWhiteSpace(str[i]))
                {
                    charTracker[str[i]]++;
                }
            }

            //count number of odd char count occurrences. if counter >1 then str is not a palindrome permutation
            int oddCounter = 0;

            for (int i = 0; i < charTracker.Length; i++)
            {
                if (charTracker[i] % 2 == 1)
                {
                    oddCounter++;
                }
                if (oddCounter > 1)
                {
                    return false;
                }
            }
            return true;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 91 Question 1.5-------------------------------------------*/

        //keep track of number of differing chars. 
        //if str1 and str2 differ by more than one character, then they are not one edit away from each other
        //we keep track of the shorter and longer string so we compare the longer string against the chars counted
        //from the shorter one. If we compare the shorter one against the longer one in our for-loop, there will be a char
        //not accounted for

        //EDIT: since the order of each char matters...we could have done an in-place while loop comparison
        //rather than having to store char counts in an array. By doing this we keep our O(n) complexity
        //but reduce our space complexity to O(1) rather than O(n).
        private bool OneEditAway(string str1, string str2)
        {
            //if they're equal...they are not one edit away
            if (str1.Equals(str2))
            {
                return false;
            }

            //if their lengths differ by more than 1 then they are more than one edit away
            if (Math.Abs(str1.Length - str2.Length) > 1)
            {
                return false;
            }

            //it is possible to get strings of same length in each variable below
            string shorterString = str1.Length < str2.Length ? str1 : str2;
            string longerString = str1.Length < str2.Length ? str2 : str1;

            int diffCounter = 0;
            int indexShorter = 0;
            int indexLonger = 0;

            while (indexShorter < shorterString.Length && indexLonger < longerString.Length)
            {
                if (shorterString[indexShorter] != longerString[indexLonger])
                {
                    diffCounter++;

                    if (shorterString.Length == longerString.Length)
                    {
                        indexShorter++;
                    }
                }
                else
                {
                    indexShorter++;
                }

                indexLonger++;

                if (diffCounter > 1)
                {
                    return false;
                }
            }

            return true;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 91 Question 1.6-------------------------------------------*/
        /*
            example input -> output:
            abcdefghijklmnop -> abcdefghijklmnop
            abcdefghhhhijk -> abcdefghhhhijk
            aaaaaaaaaabbccddeeeeffgghhi -> a10b2c2d2e4f2g2h2i1

            returns original string if the compressed string is of equal or longer length

            we can calculate length of compressed string in advance in order to avoid writing the new string
            and compare at the end should the compressed string be of equal or longer length

            this pre-calculation is also useful for instantiating a StringBuilder to the required sized
            and avoid behind-the-scenes default StringBuilder length behaviour
         */
        private string CompressString(string str)
        {
            int currentCharCount = 0;
            int compressedLength = 0;

            //calculate the length of the would-be compressed string
            for (int i = 0; i < str.Length; i++)
            {
                currentCharCount++;
                if (i + 1 >= str.Length || str[i] != str[i + 1])
                {
                    compressedLength += 1 + currentCharCount.ToString().Length;
                    currentCharCount = 0;
                }
            }

            //return original string if its compressed is of longer or equal length
            if (compressedLength >= str.Length)
            {
                return str;
            }

            StringBuilder compressedString = new StringBuilder() { Capacity = compressedLength };

            //now iterate through the original string and append chars and their counts to the compressedString variable
            for (int i = 0; i < str.Length; i++)
            {
                currentCharCount++;
                if (i + 1 >= str.Length || str[i] != str[i + 1])
                {
                    compressedString.Append(str[i] + currentCharCount.ToString());
                    currentCharCount = 0;
                }
            }


            return compressedString.ToString();
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 91 Question 1.7------------------------------------------*/
        /*
         * 90 degree matrix clockwise rotation example
         *      A           A'
         *  1 1 4 5     3 9 8 1
         *  8 5 3 6     2 8 5 1
         *  9 8 2 5  => 7 2 3 4
         *  3 2 7 9     9 5 6 5
         * 
         */

        private int[][] RotateMatrix(int[][] matrix)
        {
            //if empty matrix return original matrix
            if (matrix.Length == 0)
            {
                return matrix;
            }

            for (int layer = 0; layer < matrix.Length / 2; layer++)
            {
                int limit = matrix.Length - 1 - layer;
                for (int i = layer; i < limit; i++)
                {
                    //if not an NxN matrix return original matrix
                    if (matrix.Length != matrix[i].Length)
                    {
                        return matrix;
                    }

                    int offset = i - layer;

                    int top = matrix[layer][i];

                    matrix[layer][i] = matrix[limit - offset][layer];
                    matrix[limit - offset][layer] = matrix[limit][limit - offset];
                    matrix[limit][limit - offset] = matrix[i][limit];
                    matrix[i][limit] = top;
                }
            }

            return matrix;
        }

        private void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.Write("\n");
            }
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 91 Question 1.8------------------------------------------*/

        //MxN matrices expected as input
        private int[][] MatrixZerofy(int[][] matrix)
        {
            bool[] rowsWithZero = new bool[matrix.Length];
            bool[] columnsWithZero = new bool[matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rowsWithZero[i] = true;
                        columnsWithZero[j] = true;
                    }
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (rowsWithZero[i])
                    {
                        matrix[i][j] = 0;
                    }
                    else if (columnsWithZero[j])
                    {
                        matrix[i][j] = 0;
                    }
                }
            }

            return matrix;
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 91 Question 1.9-------------------------------------------*/
        private bool IsRotation(string s1, string s2)
        {
            if(s1.Length == s2.Length && s1.Length > 0)
            {
                string s1s1 = s1 + s1;
                return s1s1.Contains(s2);
            }
            return false;
        }

        /*------------------------------------------------------------------------------------*/

    }
}
