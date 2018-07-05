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

            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(7);
            stack.Push(5);
            stack.Push(2);
            stack.Push(6);
            stack.Push(4);

            PrintStack(stack);
            stack = SortStack(stack);
            PrintStack(stack);
        }


        private void PrintStack(Stack stack)
        {
            for(int i = stack.top; i > -1; i--)
            {
                Console.WriteLine(stack.stack[i]);
            }
            Console.WriteLine('\n');
        }

        private void PrintQueue(Queue queue)
        {
            for (int i = queue.front; i < queue.rear; i++)
            {
                Console.WriteLine(queue.queue[i]);
            }

            Console.WriteLine('\n');
        }

        private Stack SortStack(Stack inputStack)
        {
            Stack tempStack = new Stack();
            int tempVar = 0;

            while (!inputStack.IsEmpty())
            {
                tempVar = inputStack.Pop();

                while (!tempStack.IsEmpty() && tempVar > tempStack.Peek())
                {
                    inputStack.Push(tempStack.Pop());
                }

                tempStack.Push(tempVar);
            }

            return tempStack;
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
                if (IsEmpty())
                {
                    throw new ArgumentException("Stack is empty.", "stack");
                }

                return stack[top];
            }

            public bool IsEmpty()
            {
                return top == -1;
            }
        }

        class Queue
        {
            public int front, rear, size, capacity;
            public int[] queue;

            public Queue(int capacity)
            {
                this.capacity = capacity;
                size = 0;
                front = size;
                rear = front;
                queue = new int[capacity];
            }

            public bool IsFull()
            {
                return size == capacity;
            }

            public bool IsEmpty()
            {
                return size == 0;
            }

            public void Enqueue(int value)
            {
                if (IsFull())
                {
                    return;
                }

                size++;
                rear++;
                queue[rear] = value;
            }

            public int Dequeue()
            {
                if (IsEmpty())
                {
                    return default(int);
                }

                int value = queue[front];
                front++;
                size--;
                return value;
            }

            public int GetFront()
            {
                return queue[front];
            }

            public int GetRear()
            {
                return queue[rear];
            }
        }
    }
}
