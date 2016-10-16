using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollections
{
    /// <summary>
    /// Generic ring buffer queue on the array.
    /// </summary>
    public sealed class Queue<T> : ICollection<T>, ICloneable
    {
        private int capacity;
        private int head;
        private int tail;
        private T[] storage;

        private const int DefaultCapacity = 16;

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Negative value is not allowed", nameof(capacity));
                }
                capacity = value;
            }
        }
        public int Size { get; private set; }

        /// <summary>
        /// Creates a queue with the default capacity.
        /// </summary>
        public Queue() : this(DefaultCapacity)
        {

        }

        public Queue(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Clear();
            foreach (var value in values)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Creates a queue with the capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the System.Collections.Queue can contain.</param>
        public Queue(int capacity)
        {
            Capacity = capacity;
            storage = new T[Capacity];
        }

        public Queue(Queue<T> queue)
        {
            Capacity = queue.Capacity;
            storage = new T[Capacity];
            AddRange(queue);
        }

        /// <summary>
        /// Copy an array to the new array with the new capacity value.
        /// </summary>
        /// <param name="newCapacity">New capacity value.</param>
        private void SetCapacity(int newCapacity)
        {
            T[] newStorage = new T[newCapacity];
            if (Size > 0)
            {
                if (head >= tail)
                {
                    Array.Copy(storage, head, newStorage, 0, Capacity - head);
                    Array.Copy(storage, 0, newStorage, Capacity - head, tail);
                }
                else
                {
                    Array.Copy(storage, head, newStorage, 0, Size);
                }
            }
            storage = newStorage;
            head = 0;
            tail = Size;

            Capacity = newCapacity;
        }

        /// <summary>
        /// Adds a new element to the queue(tail). Capacity doubles every time after full filling.
        /// </summary>
        public void Enqueue(T element)
        {
            if (Size == Capacity)
            {
                SetCapacity(2 * Capacity);
            }

            storage[tail] = element;
            tail = (tail + 1) % Capacity;
            Size++;
        }

        /// <summary>
        /// Delete the first(head) element of the queue.
        /// </summary>
        /// <returns>Deleted element</returns>
        public T Dequeue()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("There are no elements left in the queue");
            }

            T deleted = storage[head];
            storage[head] = default(T);
            head = (head + 1) % Capacity;
            Size--;
            return deleted;
        }

        /// <summary>
        /// Returns the first(head) element of the queue.
        /// </summary>
        public T Peek()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("There are no elements left in the queue");
            }

            return storage[head];
        }

        /// <summary>
        /// Returns an elemen by index.
        /// </summary>
        /// <param name="index">Index to find element</param>
        private T GetElement(int index)
        {
            return storage[(head + index) % Capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new QueueEnumerator(this);
        }

        /// <summary>
        /// QueueEnumerator for a queue.
        /// </summary>
        private struct QueueEnumerator : IEnumerator<T>
        {
            private readonly Queue<T> queue;
            private T currentElement;

            /// <summary>
            /// -1 - Initial state, -2 - completed state.
            /// </summary>
            private int index;

            public QueueEnumerator(Queue<T> q)
            {
                queue = q;
                index = -1;
                currentElement = default(T);
            }

            public T Current
            {
                get
                {
                    if (index == -1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "The cursor is positioned before the first element");
                    }

                    if (index == -2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "Enumeration has been completed");
                    }

                    return currentElement;
                }
            }

            public void Dispose()
            {
                index = -2;
            }

            object IEnumerator.Current
            {
                get
                {
                    if (index == -1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "The cursor is positioned before the first element");
                    }

                    if (index == -2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "Enumeration has been completed");
                    }

                    return currentElement;
                }
            }

            public bool MoveNext()
            {
                if (index == -2)
                {
                    return false;
                }

                index++;
                if (index == queue.Size)
                {
                    index = -2;
                    return false;
                }

                currentElement = queue.GetElement(index);
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }

        public void Add(T item)
        {
            Enqueue(item);
        }

        public void AddRange(IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Reset all to default state.
        /// </summary>
        public void Clear()
        {
            Capacity = DefaultCapacity;
            storage = new T[Capacity];
            Size = 0;
            head = 0;
            tail = 0;
        }

        /// <summary>
        /// Type &lt;T> should implement the IEquatable&lt;T> interface 
        /// in order that the method worked correctly.
        /// </summary>
        /// <param name="item">Value to find.</param>
        /// <returns>True if queue has item otherwise returns false.</returns>
        public bool Contains(T item)
        {
            foreach (T value in this)
            {
                if (EqualityComparer<T>.Default.Equals(item, value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Copies queue values into an Array, starting at a specified
        /// index into the array.
        /// </summary>
        /// <param name="array">Array to copy in.</param>
        /// <param name="arrayIndex">Index to copy from.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (Size > 0)
            {
                if (head >= tail)
                {
                    Array.Copy(storage, head, array, arrayIndex, Capacity - head);
                    Array.Copy(storage, 0, array, Capacity - head + arrayIndex, tail);
                }
                else
                {
                    Array.Copy(storage, head, array, arrayIndex, Size);
                }
            }
        }

        /// <summary>
        /// Removes element only if this element is on the head of the queue.
        /// Type &lt;T> should implement the IEquatable&lt;T> interface 
        /// in order that the method worked correctly.
        /// </summary>
        /// <param name="item">Value to delete.</param>
        /// <returns>True if deleted otherwise return false.</returns>
        public bool Remove(T item)
        {
            T obtained = Peek();

            if (EqualityComparer<T>.Default.Equals(item, obtained))
            {
                Dequeue();
                return true;
            }

            return false;
        }

        public int Count
        {
            get
            {
                return Size;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public object Clone()
        {
            return new Queue<T>(this);
        }
    }
}
