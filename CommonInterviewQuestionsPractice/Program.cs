using System;
using System.Collections;

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
            return str;
        }
        /*------------------------------------------------------------------------------------*/
    }
}
