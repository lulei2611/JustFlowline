using JustFlowline.Abstrations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JustFlowline.Models
{
    public class FlowlineUnitCollection : ICollection<FlowlineUnit>
    {
        private readonly List<FlowlineUnit> _units = null;

        public FlowlineUnitCollection() : this(null) { }

        public FlowlineUnitCollection(IEnumerable<FlowlineUnit> items) 
        {
            _units = new List<FlowlineUnit>();
            _units.AddRange(items);
        }

        public int Count => _units.Count;

        public bool IsReadOnly => false;

        public void Add(FlowlineUnit item)
        {
            if (IsReadOnly)
                return;
            _units.Add(item);
        }

        public void Clear()
        {
            if (IsReadOnly)
                return;
            _units.Clear();
        }

        public bool Contains(FlowlineUnit item)
        {
            return _units.Contains(item);
        }

        public void CopyTo(FlowlineUnit[] array, int arrayIndex)
        {
            _units.CopyTo(array, arrayIndex);
        }

        public IEnumerator<FlowlineUnit> GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        public bool Remove(FlowlineUnit item)
        {
            if (IsReadOnly)
                return false;
            return _units.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public FlowlineUnit FindById(int id)
        {
            return _units.FirstOrDefault(p => p.Id == id);
        }
    }
}
