using System.Collections;

namespace MalachiteCore.Core;

public class DynamicObject : IDictionary<string, object>, ICloneable
{
    private DynamicNode? _head;
    private DynamicNode? _tail;
    private DynamicNode? _flag;
    private int _flagCount = -1;
    private int _size;
    private readonly Dictionary<string, DynamicNode> _keyToNode = new();

    private DynamicNode CreateNode(string key, object value)
    {
        DynamicNode dn = new(key, value)
        {
            Next = null,
            Prev = null,
        };
        return dn;
    }


    private void Put(string? key, object? value)
    {
        if (key == null || value == null)
        {
            return;
        }

        if (_keyToNode.TryGetValue(key, out var node))
        {
            node.Value = value;
            return;
        }

        DynamicNode dn = CreateNode(key, value);
        if (_head == null || _tail == null)
        {
            _head = dn;
            _tail = dn;
            _flag = _head;
        }
        else
        {
            _tail.Next = dn;
            dn.Prev = _tail;
            _tail = dn;
        }

        _size += 1;
        _keyToNode.Add(key, dn);
    }

    private object Get(string key)
    {
        if (_keyToNode.TryGetValue(key, out var node))
        {
            return node.Value;
        }

        throw new KeyNotFoundException();
    }

    public T? Get<T>(string key)
    {
        if (_keyToNode.TryGetValue(key, out var node))
        {
            return (T?)(node.Value);
        }

        return default(T);
    }

    public bool Remove(string key)
    {
        if (!_keyToNode.ContainsKey(key))
            return false;
        DynamicNode target = _keyToNode[key];
        if (target == _head)
        {
            DynamicNode? temp = target.Next;
            target.Next = null;
            if (temp != null)
            {
                temp.Prev = null;
            }

            _head = temp;
        }
        else if (target == _tail)
        {
            DynamicNode? temp = target.Prev;
            target.Prev = null;
            if (temp != null)
            {
                temp.Next = null;
            }

            _tail = temp;
        }
        else
        {
            DynamicNode? p1 = target.Prev;
            DynamicNode? p2 = target.Next;
            p1.Next = p2;
            p2.Prev = p1;
            target.Next = null;
            target.Prev = null;
        }

        _size -= 1;
        _keyToNode.Remove(key);
        return true;
    }

    public bool TryGetValue(string key, out object? value)
    {
        value = null;
        if (!ContainsKey(key))
            return false;

        value = Get(key);
        return true;
    }

    public void Add(string key, object value)
    {
        Put(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _keyToNode.ContainsKey(key);
    }

    public int Size()
    {
        return _size;
    }

    public bool HasNext()
    {
        return _flagCount < _size - 1;
    }

    public KeyValuePair<string, object> Next()
    {
        if (_head == null || _flagCount == _size - 1)
        {
            return new KeyValuePair<string, object>();
        }

        if (_flagCount == -1)
        {
            _flag = _head.Next;
            _flagCount += 1;
            return new KeyValuePair<string, object>(_head.Key, _head.Value);
        }
        else
        {
            DynamicNode? dn = _flag;
            _flag = _flag.Next;
            _flagCount += 1;
            return new KeyValuePair<string, object?>(dn.Key, dn.Value);
        }
    }

    public void Reset()
    {
        _flag = null;
        _flagCount = -1;
    }

    public virtual object this[string key]
    {
        get => Get(key);
        set => Put(key, value);
    }

    public ICollection<string> Keys => _keyToNode.Keys;
    public ICollection<object> Values => _keyToNode.Values.Select(x => x.Value).ToList();

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        while (HasNext())
            yield return Next();

        Reset();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public virtual void Add(KeyValuePair<string, object> item)
    {
        Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
        _flag = null;
        _flagCount = -1;
        _size = 0;
        _keyToNode.Clear();
    }

    public bool Contains(KeyValuePair<string, object> item) => ContainsKey(item.Key);

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
        int i = arrayIndex;
        foreach (var item in this)
        {
            array[i] = item;
            i++;
        }
    }

    public virtual bool Remove(KeyValuePair<string, object> item)
    {
        return Remove(item.Key);
    }

    public int Count => _size;
    public bool IsReadOnly => false;

    public object Clone()
    {
        throw new NotImplementedException();
    }
}

internal class DynamicNode
{
    public DynamicNode? Next;
    public DynamicNode? Prev;
    public string Key;
    public object Value;

    public DynamicNode(string key, object value)
    {
        this.Key = key;
        this.Value = value;
    }
}