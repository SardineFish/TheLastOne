using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : IEnumerable<SerializableDictionary<TKey, TValue>.KeyValuePair>
{
    public class IndexNotFoundException : Exception
    {
        public TKey Key { get; private set; }

        public IndexNotFoundException(TKey key) : base("The key is not found.")
        {
            Key = key;
        }
    }
    public class KeyValuePair
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }
        internal KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    [SerializeField]
    List<TKey> keys = new List<TKey>();
    [SerializeField]
    List<TValue> values = new List<TValue>();

    public List<TKey> Keys => keys;

    public List<TValue> Values => values;

    public int Count => keys.Count;

    public TValue this[TKey index]
    {
        get
        {
            var idx = keys.IndexOf(index);
            if (idx < 0)
            {
                if (typeof(TValue).IsClass)
                    return default(TValue);
                else
                    throw new IndexNotFoundException(index);
            }
            return values[idx];
        }
        set
        {
            var idx = keys.IndexOf(index);
            if (idx < 0)
            {
                keys.Add(index);
                values.Add(value);
            }
            else
            {
                values[idx] = value;
            }
        }
    }

    public bool ContainsKey(TKey key) => keys.Contains(key);

    public bool ContainsValue(TValue value) => values.Contains(value);

    public void Add(TKey key,TValue value)
    {
        this[key] = value;
    }

    public IEnumerator<KeyValuePair> GetEnumerator()
    {
        var idx = 0;
        foreach (var key in keys)
        {
            yield return new KeyValuePair(key, values[idx++]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var idx = 0;
        foreach (var key in keys)
        {
            yield return new KeyValuePair(key, values[idx++]);
        }
    }
}
