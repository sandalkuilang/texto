using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSGatewayWpf.Core
{
    public class ViewLocator : BaseSingleton<ViewLocator, IServiceLocator<object, object>>, IServiceLocator<object, object>
    {
        private IDictionary<object, object> container;

        public ViewLocator()
        {
            container = new Dictionary<object, object>();
        }

        public void Add(object key, object value)
        { 
            container.Add(key, value); 
        }

        public T Get<T>()
        {
            object value = default(object); 
            if (TryGetValue(typeof(T), out value))
            {
                return (T)value;
            } 
            return default(T);
        }

        public T Get<T>(string key)
        {
            object value = default(object);
            if (!string.IsNullOrEmpty(key))
            { 
                if (TryGetValue(key, out value))
                {
                    return (T)value;
                }
            }
            return default(T);
        }

        public bool ContainsKey(object key)
        {
            return container.ContainsKey(key);
        }

        public ICollection<object> Keys
        {
            get 
            {
                return container.Keys;
            }
        }

        public bool Remove(object key)
        {
            return container.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            value = default(object);
            container.TryGetValue(key, out value);
            if (value != null)
            {
                return true;
            }
            return false;
        }

        public ICollection<object> Values
        {
            get 
            {
                return container.Values;
            }
        }

        public object this[object key]
        {
            get
            {
                return this.container[key];
            }
            set
            {
                this.container[key] = value;
            }
        }

        public void Add(KeyValuePair<object, object> item)
        {
            container.Add(item);
        }

        public void Clear()
        {
            container.Clear();
        }

        public bool Contains(KeyValuePair<object, object> item)
        {
            return container.Contains(item);
        }

        public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
        {
            container.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get 
            {
                return container.Count;
            }
        }

        public bool IsReadOnly
        {
            get 
            {
                return container.IsReadOnly;
            }
        }

        public bool Remove(KeyValuePair<object, object> item)
        {
            return container.Remove(item);
        }

        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return container.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return container.GetEnumerator();
        }
    }
}
