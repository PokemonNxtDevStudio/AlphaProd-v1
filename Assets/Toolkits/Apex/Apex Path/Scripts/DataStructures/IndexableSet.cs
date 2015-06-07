namespace Apex.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Apex.Utilities;

    public class IndexableSet<T> : IIterable<T>
    {
        private HashSet<T> _hashset;
        private DynamicArray<T> _array;

        public IndexableSet()
            : this(0)
        {
        }

        public IndexableSet(int capacity)
        {
            Ensure.ArgumentInRange(() => capacity >= 0, "capacity", capacity);

            this._hashset = new HashSet<T>();
            this._array = new DynamicArray<T>(capacity);
        }

        public IndexableSet(params T[] items)
        {
            Ensure.ArgumentNotNull(items, "items");

            this._hashset = new HashSet<T>(items);
            this._array = new DynamicArray<T>(items);
        }

        public IndexableSet(IEnumerable<T> items)
        {
            Ensure.ArgumentNotNull(items, "items");

            this._hashset = new HashSet<T>(items);
            this._array = new DynamicArray<T>(items);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int count
        {
            get { return _hashset.Count; }
        }

        /// <summary>
        /// Gets the value with the specified index.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <returns>The value at the index</returns>
        public T this[int idx]
        {
            get { return _array[idx]; }
        }

        public void Add(T obj)
        {
            if (_hashset.Add(obj))
            {
                _array.Add(obj);
            }
        }

        public void AddRange(params T[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                Add(objects[i]);
            }
        }

        public void AddRange(IEnumerable<T> objects)
        {
            foreach (var obj in objects)
            {
                Add(obj);
            }
        }

        public void AddRange(IIndexable<T> objects)
        {
            for (int i = 0; i < objects.count; i++)
            {
                Add(objects[i]);
            }
        }

        public bool Remove(T obj)
        {
            if (_hashset.Remove(obj))
            {
                _array.Remove(obj);
                return true;
            }

            return false;
        }

        public bool Contains(T obj)
        {
            return _hashset.Contains(obj);
        }

        public void Sort()
        {
            _array.Sort();
        }

        public void Sort(FunctionComparer<T> comparer)
        {
            _array.Sort(comparer);
        }

        public void Sort(IComparer<T> comparer)
        {
            _array.Sort(comparer);
        }

        public void Sort(int index, int length)
        {
            _array.Sort(index, length);
        }

        public void Sort(int index, int length, FunctionComparer<T> comparer)
        {
            _array.Sort(index, length, comparer);
        }

        public void Clear()
        {
            _array.Clear();
            _hashset.Clear();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int count = this.count;
            for (int i = 0; i < count; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int count = this.count;
            for (int i = 0; i < count; i++)
            {
                yield return _array[i];
            }
        }
    }
}