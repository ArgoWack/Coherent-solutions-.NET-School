using static System.Console;

namespace Task31
{
    public static class QueueExtensions
    {
        public static Queue<T> Tail<T>(this IQueue<T> queue) where T : struct
        {
            var newQueue = new Queue<T>(queue.Capacity); // copies length of queue for new one

            // temporary queue to hold elements after skipping first one
            var tempQueue = new Queue<T>(queue.Capacity);

            // dequeues the first element and discards it
            if (!queue.IsEmpty())
            {
                queue.Dequeue();
            }

            // copies the rest of queue without changes
            while (!queue.IsEmpty())
            {
                var item = queue.Dequeue();
                tempQueue.Enqueue(item);
                newQueue.Enqueue(item);
            }

            // restores the original queue
            while (!tempQueue.IsEmpty())
            {
                queue.Enqueue(tempQueue.Dequeue());
            }

            return newQueue;
        }
    }
}
