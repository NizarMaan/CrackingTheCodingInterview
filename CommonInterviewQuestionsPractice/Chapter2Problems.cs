using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterviewQuestionsPractice
{
    class Chapter2Problems
    {
        private static Chapter2Problems instance = null;

        public static Chapter2Problems GetInstance()
        {
            if (instance == null)
            {
                instance = new Chapter2Problems();
            }

            return instance;
        }

        public void RunProblems()
        {
            Console.WriteLine("===========================Chapter 2=============================");
            Node head = new Node(2);
            int[] values = new int[] { 5, 6, 6, 6, 3, 2, 1, 2, 1, 3, 4, 8, 4, 9, 10, 7, 7, 9, 5, 8};

            PopulateLinkedList(head, values);
            PrintLinkedListValues(head);
            RemoveDuplicates(head);
            PrintLinkedListValues(head);
            Console.WriteLine(KthToLast(head, 4).Value);
            Node middleNode = head.Next.Next.Next.Next.Next;
            DeleteMiddleNode(middleNode);
            PrintLinkedListValues(head);
            head = Partition(head, 6);
            PrintLinkedListValues(head);

            int[] num1 = new int[] { 5, 0 };
            Node head2 = new Node(6);
            PopulateLinkedList(head2, num1);
            Node head3 = new Node(3);
            PopulateLinkedList(head3, num1);
            Node sum = SumOfLinkedLists(head2, head3);
            PrintLinkedListValues(sum);

            Node intersection = new Node(99);

            Node[] llVals = new Node[] { new Node(1), new Node(4), new Node(5), intersection, new Node(6) };
            Node ll1 = new Node(6);
            PopulateLinkedList(ll1, llVals);

            llVals = new Node[] { new Node(1), new Node(4), new Node(5), intersection };
            Node ll2 = new Node(3);

            PopulateLinkedList(ll2, llVals);

            PrintLinkedListValues(ll1);
            PrintLinkedListValues(ll2);

            PrintLinkedListValues(FindIntersectingNode(ll1, ll2));

            Node ll = new Node(1);
            Node looper = new Node(33);
            llVals = new Node[] { new Node(7), new Node(4), new Node(5), looper, new Node(2), new Node(6), looper};
            PrintLinkedListValues(DetectLoop(ll));
        }

        /*----------------------pg. 94 Question 2.1-------------------------------------------*/
        private class Node {
            private int value;
            private Node next;

            public int Value
            {
                get
                {
                    return value;
                }
                set
                {
                    this.value = value;
                }
            }

            public Node Next{
                get
                {
                    return next;
                }
                set
                {
                    next = value;
                }
            }

            public Node(int value)
            {
                this.value = value;
                next = null;
            }

            public Node(int value, Node next)
            {
                this.value = value;
                this.next = next;
            }
        }

        //populate with integers
        private void PopulateLinkedList(Node head, int[] values)
        {
            Node currentNode = head;
            for (int i = 0; i < values.Length; i++)
            {
                currentNode.Next = new Node(values[i]);
                currentNode = currentNode.Next;
            }
        }

        //populate with Nodes
        private void PopulateLinkedList(Node head, Node[] values)
        {
            Node currentNode = head;
            for (int i = 0; i < values.Length; i++)
            {
                currentNode.Next = values[i];
                currentNode = currentNode.Next;
            }
        }

        private void PrintLinkedListValues(Node head)
        {
            Node currentNode = head;
            while(currentNode != null)
            {
                if(currentNode.Next == null)
                {
                    Console.WriteLine("{0}", currentNode.Value);
                }
                else
                {
                    Console.Write("{0}, ", currentNode.Value);
                }

                currentNode = currentNode.Next;
            }
        }

        //without using a data structure to keep track of visisted node values
        private void RemoveDuplicates(Node head)
        {
            Node pointer1 = head;
            Node pointer2 = pointer1;

            while(pointer1 != null)
            {
                while(pointer2.Next != null)
                {
                    if(pointer1.Value == pointer2.Next.Value)
                    {
                        pointer2.Next = pointer2.Next.Next;
                    }
                    else
                    {
                        pointer2 = pointer2.Next;
                    }
                }

                pointer1 = pointer1.Next;
                pointer2 = pointer1;
            }
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 223 Question 2.2-------------------------------------------*/
        private Node KthToLast(Node head, int positionToLast)
        {
            if(head == null)
            {
                return head;
            }

            int length = 0;
            Node pointer = head;

            //compute lenght of linked list
            while (pointer != null)
            {
                length++;
                pointer = pointer.Next;
            }

            //if positionToLast longer than linked list's length
            if(positionToLast > length)
            {
                return head;
            }

            int count = 0;
            int limit = length - positionToLast;
            pointer = head;

            while(count < limit)
            {
                pointer = pointer.Next;
                count++;
            }

            return pointer;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 94 Question 2.3-------------------------------------------*/

        private void DeleteMiddleNode(Node middleNode)
        {
            //if middleNode not the last node
            if(middleNode != null && middleNode.Next != null)
            {
                middleNode.Value = middleNode.Next.Value;
                middleNode.Next = middleNode.Next.Next;
            }
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 94 Question 2.4-------------------------------------------*/
        //changing one of the objects within the linked list changes its value across newHead, tail, and head
        private Node Partition(Node head, int partition)
        {
            Node newHead = head;
            Node tail = head;

            while(head != null)
            {
                Node next = head.Next;
                if(head.Value < partition)
                {
                    head.Next = newHead;
                    newHead = head;
                }
                else
                {
                    tail.Next = head;
                    tail = head;
                }

                head = next;
            }

            return newHead;
        }

        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 95 Question 2.5-------------------------------------------*/
        /// <summary>
        /// Returns sum of the numbers represented by two linked lists in forward order. 
        /// Each node contains a 1 digit integer
        /// </summary>
        /// <param name="head1">representing a number in forward order e.g. 1 -> 6 -> 7 represents the number 167</param>
        /// <param name="head2">representing a number in forward order e.g. 1 -> 6 -> 7 represents the number 167</param>
        /// <returns>Linked list representing the sum of the numbers represented by the two arguments in forward order</returns>
        private Node SumOfLinkedLists(Node head1, Node head2)
        {
            int sum;
            int length1 = 0;
            int length2 = 0;
            int value1 = 0;
            int value2 = 0;
            Node sumAsLinkedList = new Node(-1);
            Node pointer1 = head1;
            Node pointer2 = head2;

            while (true)
            {
                if(pointer1 != null)
                {
                    length1++;
                    pointer1 = pointer1.Next;
                }
                else if(pointer2 != null)
                {
                    length2++;
                    pointer2 = pointer2.Next;
                }
                else
                {
                    break;
                }
            }

            pointer1 = head1;
            while(pointer1 != null)
            {
                value1 += (int)(pointer1.Value * Math.Pow(10, length1 - 1));
                length1--;
                pointer1 = pointer1.Next;
            }

            pointer2 = head2;
            while (pointer2 != null)
            {
                value2 += (int)(pointer2.Value * Math.Pow(10, length2 - 1));
                length2--;
                pointer2 = pointer2.Next;
            }

            sum = value1 + value2;
            string sumStr = sum.ToString();
            Node pointer = new Node(-1);

            for(int i = 0; i < sumStr.Length; i++)
            {
                if (i == 0)
                {
                    sumAsLinkedList = new Node((int)Char.GetNumericValue(sumStr[i]));
                    pointer = sumAsLinkedList;
                }
                else
                {
                    pointer.Next = new Node((int)Char.GetNumericValue(sumStr[i]));
                    pointer = pointer.Next;
                }
            }

            return sumAsLinkedList;
        }
        /*------------------------------------------------------------------------------------*/
        /*----------------------pg. 95 Question 2.6-------------------------------------------*/
        /// <summary>
        /// Returns true or false if the Linked list of integers is a palindrome e.g. 1 -> 1 -> 1 is a palindrome
        /// </summary>
        /// <param name="head">head of linked list to check</param>
        /// <returns>
        /// true - if is palindrome
        /// false - if not palindrome
        /// </returns>
        private bool IsPalindrome(Node head)
        {
            return false;
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 95 Question 2.7-------------------------------------------*/

        /*this solution is incorrect as it doesnt account for some cases where linked lists that differ in length
        private Node FindIntersectingNode(Node head1, Node head2)
        {
            Node pointer1 = head1;
            Node pointer2 = head2;
            Node intersection = null;

            while (true)
            {
                if(pointer1 == pointer2)
                {
                    return pointer1;
                }

                if (pointer1 != null)
                {
                    pointer1 = pointer1.Next;
                }
                if (pointer1 == null)
                {
                    intersection = intersection == null ? pointer2.Next : intersection;
                }

                if (pointer2 != null)
                {
                    pointer2 = pointer2.Next;
                }
                
                if(pointer2 == null)
                {
                    intersection = intersection == null ? pointer1 : intersection;
                }

                if (pointer1 == null && pointer2 == null)
                {
                    if(pointer1 == pointer2)
                    {
                        return intersection;
                    }
                    else
                    {
                        return new Node(-1);
                    }
                }
            }
        }/*
        /*------------------------------------------------------------------------------------*/

        //we can get the difference in lengths between both lists and move the pointer of the longest list to the index length1 - length2...
        private Node FindIntersectingNode(Node head1, Node head2)
        {
            int length1 = GetLinkedListLength(head1);
            int length2 = GetLinkedListLength(head2);

            Node longest = length1 > length2 ? head1 : head2;
            Node shortest = length1 > length2 ? head2 : head1;

            int difference = Math.Abs(length1 - length2);

            for(int i = 0; i < difference; i++)
            {
                longest = longest.Next;
            }

            while (true)
            {
                if (longest == shortest)
                {
                    return longest;
                }

                if(shortest != null)
                {
                    shortest = shortest.Next;
                }

               longest = longest.Next;
            }
        }

        private int GetLinkedListLength(Node head)
        {
            int length = 0;

            while (head != null)
            {
                head = head.Next;
                length++;
            }

            return length;
        }
        /*------------------------------------------------------------------------------------*/

        /*----------------------pg. 95 Question 2.8-------------------------------------------*/
        private Node DetectLoop(Node head)
        {

            return new Node(-1); //no loop found
        }
        /*------------------------------------------------------------------------------------*/
    }
}
