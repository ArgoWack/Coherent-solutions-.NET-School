using static System.Console;

namespace Task31
{
    //array-based approach, if I understood correctly we were meant to chose one of them, not make 3 versions right?
    public class Queue<T> : IQueue<T> where T : struct
    {
        private T[] items;
        private int head;
        private int tail;
        private int count;
        private int capacity;

        public int Capacity => capacity;

        public Queue(int capacity)
        {
            this.capacity = capacity;
            items = new T[capacity];
            head = 0;
            tail = -1;
            count = 0;
        }

        public void Enqueue(T item)
        {
            if (count == capacity)
            {
                throw new InvalidOperationException("Queue is full. Cannot add more items.");
            }
            tail = (tail + 1) % capacity;
            items[tail] = item;
            count++;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty. Cannot remove any items.");
            }
            T item = items[head];
            head = (head + 1) % capacity;
            count--;
            return item;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }
    }
}
