using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{

    public class MapHash
    {
        private List<MapHashValue> _values;

        public MapHash()
        {
            _values = new List<MapHashValue>();
        }

        public MapHash(MapHash var3)
        {
            try
            {
                _values = new List<MapHashValue>();
                foreach (MapHashValue t1 in var3._values)
                {
                    foreach (string t2 in t1.Second)
                        this.put(t1.First, t2);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public int Count
        {
            get
            {
                return _values.Count;
            }
        }

        public List<string> this[string i]
        {
            get
            {
                try
                {
                    foreach (MapHashValue kv in _values)
                    {
                        if (kv.First.Equals(i))
                        {
                            return kv.Second;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                return null;
            }
            set
            {
                try
                {
                    foreach (MapHashValue kv in _values)
                    {
                        if (kv.First.Equals(i))
                        {
                            foreach (string t in value)
                                kv.SetValue(t);
                            return;
                        }
                    }
                    MapHashValue mapHashValue = new MapHashValue();
                    mapHashValue.First = i;
                    foreach (string t in value)
                        mapHashValue.SetValue(t);
                    this._values.Add(mapHashValue);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public void Add(MapHashValue value)
        {
            try
            {
                //_values.Add(value);
                if (value == null) return;
                if (value.Second == null) return;

                foreach (string t in value.Second)
                {
                    put(value.First, t);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Add(string key, string value)
        {
            put(key, value);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public void Remove(MapHashValue value)
        {
            _values.Remove(value);
        }

        public void Remove(string _key)
        {
            try
            {
                foreach (MapHashValue kv in _values)
                {
                    if (kv.First.Equals(_key))
                    {
                        _values.Remove(kv);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public IEnumerator<MapHashValue> GetEnumerator()
        {

            for (int i = 0; i <= _values.Count; i++)
            {
                if (i == _values.Count) yield break; // Выход из итератора, если закончится алфавит
                yield return _values[i];
            }
        }
        internal List<string> get(string i)
        {
            try
            {
                foreach (MapHashValue kv in _values)
                {
                    if (kv.First.Equals(i))
                    {
                        return kv.Second;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }
        internal void put(string i, string value)
        {
            try
            {
                foreach (MapHashValue kv in _values)
                {
                    if (kv.First.Equals(i))
                    {
                        kv.SetValue(value);
                        return;
                    }
                }

                MapHashValue mapValue = new MapHashValue(i, value);
                _values.Add(mapValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public bool Contains(string key)
        {
            try
            {
                foreach (MapHashValue kv in _values)
                {
                    if (kv.First.Equals(key)) return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false;
        }
        public List<string> GetValues()
        {
            try
            {
                List<string> values = new List<string>();
                foreach (MapHashValue kv in _values)
                    foreach (string t1 in kv.Second)
                        values.Add(t1);
                return values;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
        public List<string> GetKeys()
        {
            try
            {
                List<string> values = new List<string>();
                foreach (MapHashValue kv in _values)
                    values.Add(kv.First);
                return values;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        internal void Add(string i, List<string> value)
        {
            try
            {
                foreach (string t in value)
                    this.put(i, t);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public bool TryGetValue(string key, out List<string> value)
        {
            try { 
            foreach (MapHashValue kv in _values)
            {
                if (kv.First.Equals(key))
                {
                    value = kv.Second;
                    return true;

                }
            }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            value = null;
            return false;
        }
        public bool ContainsKey(string _key)
        {
            return Contains(_key);
        }
        internal bool isEmpty()
        {
            return (_values.Count == 0);
        }
    }
}
