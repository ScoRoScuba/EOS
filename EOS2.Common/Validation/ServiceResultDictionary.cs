namespace EOS2.Common.Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public class ServiceResultDictionary : IDictionary<string, ServiceState>
    {
        private readonly Dictionary<string, ServiceState> innerState = new Dictionary<string, ServiceState>();

        public bool HasErrors
        {
            get
            {
                return Values.Count != 0;
            }
        }

        public ICollection<ServiceState> Values
        {
            get { return innerState.Values; }
        }

        public ICollection<string> Keys
        {
            get
            {
                return innerState.Keys;
            }
        }

        public int Count
        {
            get { return innerState.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, ServiceState>)innerState).IsReadOnly; }
        }

        public ServiceState this[string key]
        {
            get
            {
                ServiceState value;
                innerState.TryGetValue(key, out value);
                return value;
            }

            set
            {
                innerState[key] = value;
            }
        }

        public void Add(string key, ServiceState value)
        {     
            innerState.Add(key, value);       
        }

        public void Add(KeyValuePair<string, ServiceState> item)
        {
            ((IDictionary<string, ServiceState>)innerState).Add(item);
        }

        public void AddModelError(string key, Exception exception)
        {
          this.GetServiceStateForKey(key).Errors.Add(exception);
        }

        public void AddModelError(string key, string message)
        {
          this.GetServiceStateForKey(key).Errors.Add(message);
        }

        public bool ContainsKey(string key)
        {
            return innerState.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return innerState.Remove(key);
        }

        public bool Remove(KeyValuePair<string, ServiceState> item)
        {
            return ((IDictionary<string, ServiceState>)innerState).Remove(item);
        }

        public bool TryGetValue(string key, out ServiceState value)
        {
            return innerState.TryGetValue(key, out value);
        }

        public void Clear()
        {
            innerState.Clear();
        }

        public bool Contains(KeyValuePair<string, ServiceState> item)
        {
            return ((IDictionary<string, ServiceState>)innerState).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, ServiceState>[] array, int arrayIndex)
        {
            ((IDictionary<string, ServiceState>)innerState).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ServiceState>> GetEnumerator()
        {
            return innerState.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)innerState).GetEnumerator();
        }

        public void SetServiceState(string key, string value)
        {
            this.GetServiceStateForKey(key).Value = value;
        }

        public void SetServiceState(string key, int value)
        {
            this.GetServiceStateForKey(key).Value = value.ToString(CultureInfo.InvariantCulture);
        }

        public void SetServiceState(string key, bool value)
        {
            this.GetServiceStateForKey(key).Value = value.ToString();
        }

        private ServiceState GetServiceStateForKey(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            ServiceState serviceState;

            if (!TryGetValue(key, out serviceState))
            {
                serviceState = new ServiceState();
                this[key] = serviceState;
            }

            return serviceState;
        }
    }
}
