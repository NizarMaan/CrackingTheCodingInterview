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

        private void PopulateLinkedList(Node head, int[] values)
        {
            Node currentNode = head;
            for (int i = 0; i < values.Length; i++)
            {
                currentNode.Next = new Node(values[i]);
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
        private Node Partition(Node head, int partition)
        {
            return head;
        }
 
        
        /*------------------------------------------------------------------------------------*/
    }
}
