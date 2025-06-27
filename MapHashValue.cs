using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfExplorer
{
    public class MapHashValue
    {

        private string _key;
        private List<string> _value;


        public MapHashValue()
        {
            _key = "";
            _value = new List<string>();
        }

        public MapHashValue(string key, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) { return; }
                _key = key;
                if (string.IsNullOrEmpty(value) == null) { return; }
                if (_value != null)
                    foreach (string t1 in _value)
                    {
                        if (!string.IsNullOrEmpty(t1))
                        {
                            if (t1.Equals(value))
                            {
                                return;
                            }
                        }
                    }
                else
                    _value = new List<string>();
                _value.Add(value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (!(obj is MapHashValue)) { return false; }
                if (obj == null) { return false; }
                MapHashValue other = (MapHashValue)obj;
                if (this._key.Equals(other._key))
                {
                    if (_value.Count != other._value.Count)
                        return false;
                    for (int i = 0; i < _value.Count; i++)
                    {
                        if (!this._value[i].Equals(other._value[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false;
        }

        public string First
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        /*   public override int GetHashCode()
           {
               return HashCode.Combine(_key, _value);
           }*/

        public List<string> Second
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public void SetValue(string value)
        {
            try
            {

                if (string.IsNullOrEmpty(value) == null) { return; }

                foreach (string t1 in _value)
                {
                    if (!string.IsNullOrEmpty(t1))
                    {
                        if (t1.Equals(value))
                        {
                            return;
                        }
                    }
                }
                _value.Add(value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public string[] ToString()
        {
            string[] t1=null;

            int count = _value.Count;

            t1 = new string[count];

            for (int i = 0; i < count; i++)
            {
                t1[i] = _key+"*" +_value[i];
            }

            return t1;
        }
    }
}
