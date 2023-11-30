using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>, IEnumerable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        private class DoublyLinkedListEnumerator : IEnumerator<T>
        {
            private readonly DoublyLinkedList<T> _list;
            private DoublyLinkedList<T>.Node _currentNode;

            public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
            {
                _list = list;
                _currentNode = null;
            }

            public T Current => _currentNode.Value;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_currentNode == null)
                {
                    _currentNode = _list._head;
                }
                else
                {
                    _currentNode = _currentNode.Next;
                }

                return _currentNode != null;
            }

            public void Reset() => _currentNode = null;
        }

        private Node _head;
        private Node _tail;

        public int Length { get; private set; }

        public void Add(T e)
        {
            Node newNode = new Node(e);
            if (_tail == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
                _tail = newNode;
            }
            Length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }

            Node newNode = new Node(e);

            if (index == Length)
            {
                Add(e);
                return;
            }

            if (index == 0)
            {
                newNode.Next = _head;
                _head.Prev = newNode;
                _head = newNode;
                Length++;
                return;
            }

            Node currNode = GetNodeAtIndex(index);

            newNode.Prev = currNode.Prev;
            newNode.Next = currNode;
            currNode.Prev.Next = newNode;
            currNode.Prev = newNode;

            Length++;
        }

        public T ElementAt(int index)
        {
            Node node = GetNodeAtIndex(index);
            return node.Value;
        }

        public void Remove(T item)
        {
            Node currNode = _head;
            while (currNode != null)
            {
                if (currNode.Value.Equals(item))
                {
                    RemoveNode(currNode);
                    break;
                }
                currNode = currNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            Node nodeToRemove = GetNodeAtIndex(index);
            RemoveNode(nodeToRemove);
            return nodeToRemove.Value;
        }

        private void RemoveNode(Node node)
        {
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                _head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                _tail = node.Prev;
            }

            Length--;
        }

        private Node GetNodeAtIndex(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            Node currNode = _head;
            for (int i = 0; i < index; i++)
            {
                currNode = currNode.Next;
            }

            return currNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
