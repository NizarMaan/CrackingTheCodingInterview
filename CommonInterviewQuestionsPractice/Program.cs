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
            Console.WriteLine(StringWithUniqueChars("test")); //false
            Console.WriteLine(StringWithUniqueChars("abcd")); //true
            Console.WriteLine(IsPermutation("test", "ttse")); //true
            Console.WriteLine(IsPermutation("test", "tese")); //false

            int a = 1;
            int b = 2;
            Console.WriteLine(a + " " + b);
            Swap(a, b);
            Console.WriteLine(a + " " + b);

            Console.WriteLine(URLify("I like to program things                                                            ", 24));

            int[] testList = new int[] {2, 7, -2, 0, 2, 1, 3, 11, 5, 4, 6, 12, 10};
            Console.WriteLine(HasPair(testList, 8)); //pair {7, 1}
            Console.WriteLine(HasPair(testList, -2)); //pair {-2, 0}
            Console.WriteLine(HasPair(testList, 24)); //no pair
            
            string[] palindromeTest = new string[] {"cvcii", "Anna", "akyak", "tcao cta", "racecar", "abcdefg", "mom", "noon" };
                                                     //true   //false  //true  //true     //true      //false   //true  //true
            foreach(string str in palindromeTest)
            {
                Console.WriteLine(str + ",  " + IsPalindromePermutation(str));
            }

            Console.WriteLine(OneEditAway("pale", "ple")); //true
            Console.WriteLine(OneEditAway("pales", "pale")); //true
            Console.WriteLine(OneEditAway("pale", "bale")); //true
            Console.WriteLine(OneEditAway("pale", "bae")); //false
            Console.WriteLine(OneEditAway("pale", "paleee")); //false
            Console.WriteLine(OneEditAway("pale", "pale")); //false

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
        //TODO: also check each char count. 
        public static bool IsPermutation(string str1, string str2)
        {
            if(str1.Length != str2.Length)
            {
                return false;
            }

            int[] charTracker = new int[128]; //assuming ASCII character codes, 128 of them

            //count each character in str1
            for(int i = 0; i < str1.Length; i++)
            {
                charTracker[str1[i]]++;
            }
            
            //for each char in str2 decrease the corresponding index that holds the counter in charTracker
            for(int i = 0; i < str2.Length; i++)
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
        public static string URLify(string str, int trueLength) //param: trueLength is the length of the string ignoring empty space after the last non-empty char
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

        /*----------------------pg. 91 Question 1.4-------------------------------------------*/

        /*an even length palindrome must have an even amount of each letter, an odd lengthed palindrome
        has at least (typically the middle character) one letter which does not appear an even amount of times
        so we can count the amount of characters which appear an odd amount of times. If there is more than 1 char
        that appears an odd amount of times then the string is not a permutation of a palindrome
        */
        public static bool IsPalindromePermutation(string str)
        {
            int[] charTracker = new int[128]; //assuming ASCII character set values 0-128

            //count each char
            for(int i = 0; i < str.Length; i++)
            {
                //ignore space counts
                if (!Char.IsWhiteSpace(str[i]))
                {
                    charTracker[str[i]]++;
                }
            }

            //count number of odd char count occurrences. if counter >1 then str is not a palindrome permutation
            int oddCounter = 0;
       
            for(int i = 0; i < charTracker.Length; i++)
            {
                if(charTracker[i] % 2 == 1)
                {
                    oddCounter++;
                }
                if(oddCounter > 1)
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
        public static bool OneEditAway(string str1, string str2)
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

            string shorterString = str1.Length < str2.Length ? str1 : str2;
            string longerString = str1.Length < str2.Length ? str2 : str1;

            int[] charTracker = new int[128]; //assuming ASCII Characters
            
            //increase counter for each character that we encounter at its corresponding index in charTracker array
            for(int i = 0; i < shorterString.Length; i++)
            {
                charTracker[shorterString[i]]++;
            }

            int diffCounter = 0;
            for (int i = 0; i < longerString.Length; i++)
            {
                charTracker[longerString[i]]--;

                if (charTracker[longerString[i]] < 0)
                {
                    diffCounter++;
                }

                if(diffCounter > 1)
                {
                    return false;
                }
            }

            return true;
        }

        /*------------------------------------------------------------------------------------*/
    }
}
