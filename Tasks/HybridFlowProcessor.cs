using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> _storage = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (_storage.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T dequeuedItem = _storage.ElementAt(0);
            _storage.RemoveAt(0);
            return dequeuedItem;
        }

        public void Enqueue(T item)
        {
            _storage.Add(item);
        }

        public T Pop()
        {
            if (_storage.Length == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            T poppedItem = _storage.ElementAt(_storage.Length - 1);
            _storage.RemoveAt(_storage.Length - 1);
            return poppedItem;
        }

        public void Push(T item)
        {
            _storage.Add(item);
        }
    }
}