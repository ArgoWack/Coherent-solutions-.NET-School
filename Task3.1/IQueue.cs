using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task31
{
    public interface IQueue<T> where T : struct
    {
        void Enqueue(T item);
        T Dequeue();
        bool IsEmpty();
        int Capacity { get; }
    }
}
