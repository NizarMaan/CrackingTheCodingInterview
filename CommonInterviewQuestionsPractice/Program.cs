using System;
using System.Collections;
using System.Text;

namespace CommonInterviewQuestionsPractice
{
    /*----Cracking the Coding Interview----*/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StringWithUniqueChars("test"));
            Console.WriteLine(IsPermutation("test", "ttes"));

            int a = 1;
            int b = 2;
            Console.WriteLine(a + " " + b);
            Swap(a, b);
            Console.WriteLine(a + " " + b);

            Console.WriteLine(URLify("I like to program things                                                             ", 24));

            int[] testList = new int[] {2, 7, -2, 0, 2, 1, 3, 11, 5, 4, 6, 12, 10};
            Console.WriteLine(HasPair(testList, 8));
            Console.WriteLine(HasPair(testList, -2));
            Console.WriteLine(HasPair(testList, 24));
        }

        /*----------------------pg. 90 Question 1.1-------------------------------------------*/
        public static bool StringWithUniqueChars(string str)
        {
            Hashtable map = new Hashtable();

            for(int i = 0; i < str.Length; i++)
            {
                if(map[CharToInt(str[i])] != null)
                {
                    return false;
                }

                map.Add(CharToInt(str[i]), str[i]);
            }
                
            return true;
        }

        public static int CharToInt(char c)
        {
            return c - '0';
        }
        /*------------------------------------------------------------------------------------*/

        //testing if pass by reference or value
        public static void Swap(int a, int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /*----------------------pg. 90 Question 1.2-------------------------------------------*/
        public static bool IsPermutation(string str1, string str2)
        {
            if(str1.Length != str2.Length)
            {
                return false;
            }

            Hashtable map = new Hashtable();

            for (int i = 0; i < str1.Length; i++)
            {
                if (map[CharToInt(str1[i])] != null)
                {
                    break;
                }

                map.Add(CharToInt(str1[i]), str1[i]);
            }

            for (int i = 0; i < str2.Length; i++)
            {
                if (map[CharToInt(str2[i])] == null)
                {
                    return false;
                }
            }

            return true;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 90 Question 1.2-------------------------------------------*/
        public static string URLify(string str, int trueLength) //param: trueLength is the length of the string ignoring empty space after the last non-empty char
        {           
            int spacesCount = 0;
            char[] specialChar = new char[] { '%', '2', '0' };
            StringBuilder newString = new StringBuilder();
            newString.Length = str.Length;

            //count spaces
            for (int i = 0; i < trueLength; i++)
            {
                if(Char.IsWhiteSpace(str[i]))
                {
                    spacesCount++;
                }
            }

            int newTrueLength = trueLength + (2 * spacesCount);

            //shift characters down
            for (int i = trueLength - 1; i >= 0; i--)
            {
                if(spacesCount > 0)
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
                if(counter == specialChar.Length)
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
        public static string HasPair(int[] list, int desiredSum)
        {
            Hashtable numTracker = new Hashtable();
            for(int i = 0; i < list.Length; i++)
            {
                if(numTracker[desiredSum - list[i]] != null)
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
    }
}
