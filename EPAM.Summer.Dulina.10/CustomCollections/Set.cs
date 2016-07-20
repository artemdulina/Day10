using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    /// <summary>
    /// Потом List&lt;T> заменю на BinaryTree&lt;T> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Set<T> : ICollection<T> where T : class
    {
        private readonly List<T> storage = new List<T>();

        public Set()
        {

        }

        public Set(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            AddRange(values);
        }

        public Set<T> Union(Set<T> another)
        {
            if (another == null)
            {
                return new Set<T>(storage);
            }

            Set<T> union = new Set<T>(storage);

            foreach (var item in another)
            {
                if (!union.Contains(item))
                {
                    union.Add(item);
                }
            }

            return union;
        }

        public Set<T> Intersection(Set<T> another)
        {
            if (another == null)
            {
                return new Set<T>();
            }

            Set<T> union = new Set<T>();

            foreach (var item in another)
            {
                if (storage.Contains(item))
                {
                    union.Add(item);
                }
            }

            return union;
        }

        public Set<T> Difference(Set<T> another)
        {
            if (another == null)
            {
                return new Set<T>(storage);
            }

            Set<T> union = new Set<T>(storage);

            foreach (var item in another)
            {
                union.Remove(item);
            }

            return union.Count == 0 ? null : union;
        }

        public Set<T> SymmetricDifference(Set<T> another)
        {
            if (another == null)
            {
                return new Set<T>(storage);
            }

            Set<T> union = new Set<T>();

            foreach (var item in storage)
            {
                if (!another.Contains(item))
                {
                    union.Add(item);
                }
            }

            foreach (var item in another)
            {
                if (!storage.Contains(item))
                {
                    union.Add(item);
                }
            }

            return union;
        }

        public bool IsSubset(Set<T> another)
        {
            if (another == null)
            {
                return false;
            }

            return Difference(another) == null;
        }

        public static Set<T> operator -(Set<T> lhs, Set<T> rhs)
        {
            return lhs.Difference(rhs);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (!storage.Contains(item))
            {
                storage.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                if (!storage.Contains(item))
                {
                    storage.Add(item);
                }
            }
        }

        public void Clear()
        {
            storage.Clear();
        }

        public bool Contains(T item)
        {
            return storage.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(storage.ToArray(), 0, array, arrayIndex, storage.Count);
        }

        public bool Remove(T item)
        {
            return storage.Remove(item);
        }

        public int Count
        {
            get
            {
                return storage.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}
