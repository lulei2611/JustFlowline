using System;
using System.Collections;
using System.Collections.Generic;

namespace JustFlowline.Models
{
    public class ExecutionPointerCollection : ICollection<ExecutionPointer>
    {
        private readonly List<ExecutionPointer> _executionPointers = null;

        public ExecutionPointerCollection() 
        { 
            _executionPointers = new List<ExecutionPointer>();
        }

        public int Count => _executionPointers.Count;

        public bool IsReadOnly => false;

        public void Add(ExecutionPointer item)
        {
            _executionPointers.Add(item);
        }

        public void Clear()
        {
            _executionPointers.Clear();
        }

        public bool Contains(ExecutionPointer item)
        {
            return _executionPointers.Contains(item);
        }

        public void CopyTo(ExecutionPointer[] array, int arrayIndex)
        {
            _executionPointers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ExecutionPointer> GetEnumerator()
        {
            return _executionPointers.GetEnumerator();
        }

        public bool Remove(ExecutionPointer item)
        {
            return _executionPointers.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
