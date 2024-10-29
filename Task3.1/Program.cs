using static System.Console;

/*
Task 3.1. 
A queue is an abstract data type organized according to the FIFO principle and implements the following operations:
Enqueue(e) – adding an element to the queue.
Dequeue() – removes an element from the queue.
IsEmpty() – checking the queue for emptiness.
To do:
Create a universal interface IQueue<T> that describes operations with a queue storing elements of value types.
Create a generic Queue<T> class that implements the IQueue<T> interface. In the Queue<T> class, use one of the queue implementations (to choose from):
array based
based on a linked list
based on two stacks
Create a universal extension method Tail<T>() for the IQueue<T> interface. This method returns a new queue consisting of the elements of the original parameter queue minus the first element. For example, for a queue of elements 1, 3, 5, 7, the Tail<T>() extension method will return a queue of elements 3, 5, 7.
Test the operation of the created types and methods in a console application.

(*) If you already know something about exceptions and exceptional situations, throw exceptions when an attempt is made to remove an element from an empty queue, or when an element is added when the maximum capacity of the queue has been reached. If you don’t know about exceptions, do nothing when trying to add to a full queue; when trying to extract from an empty queue, return the default(T) value.
*/

namespace Task31
{
    class Program
    {
        public static void RunTask31()
        {

        }
    }
}