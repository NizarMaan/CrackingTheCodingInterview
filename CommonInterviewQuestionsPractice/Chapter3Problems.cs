using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterviewQuestionsPractice
{
    class Chapter3Problems
    {
        private static Chapter3Problems instance = null;

        public static Chapter3Problems GetInstance()
        {
            if (instance == null)
            {
                instance = new Chapter3Problems();
            }

            return instance;
        }

        public void RunProblems()
        {
            Console.WriteLine("===========================Chapter 3=============================");
        }

        class Stack
        {
            public int top = -1;
            public int[] stack;
            private const int DEFAULT_CAPACITY = 20;

            public Stack()
            {
                stack = new int[DEFAULT_CAPACITY];
            }

            public Stack(int size)
            {
                stack = new int[size];
            }

            public void Push(int value)
            {
                top++;
                stack[top] = value;
            }

            public int Pop()
            {
                int result = Peek();

                stack[top] = default(int);
                top--;

                return result;
            }

            public int Peek()
            {
                if (Empty())
                {
                    throw new ArgumentException("Stack is empty.", "stack");
                }

                return stack[top];
            }

            public bool Empty()
            {
                return top == -1;
            }
        }
    }
}
