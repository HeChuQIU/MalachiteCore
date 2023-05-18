using System.Collections;
using MalachiteCore.Core;

namespace MalachiteCore.Core;

public class DynamicObject : IEnumerable<object>
{
    DynamicNode? head;
    DynamicNode? tail;
    DynamicNode? flag;
    int flagCount = -1;
    int size = 0;
    Dictionary<string, DynamicNode> keyToNode = new();

    private DynamicNode createNode(string key, object value)
    {
        DynamicNode dn = new DynamicNode();
        dn.Next = null;
        dn.Prev = null;
        dn.Value = value;
        dn.Key = key;
        return dn;
    }


    public void Put(string key, object value)
    {
        if (key == null || value == null)
        {
            return;
        }

        if (keyToNode.ContainsKey(key))
        {
            keyToNode[key].Value = value;
            return;
        }

        DynamicNode dn = createNode(key, value);
        if (head == null)
        {
            head = dn;
            tail = dn;
            flag = head;
        }
        else
        {
            tail.Next = dn;
            dn.Prev = tail;
            tail = dn;
        }

        size += 1;
        keyToNode.Add(key, dn);
    }

    public object Get(string key)
    {
        if (keyToNode.ContainsKey(key))
        {
            return keyToNode[key].Value;
        }

        return null;
    }

    public T Get<T>(string key)
    {
        if (keyToNode.ContainsKey(key))
        {
            return (T)(keyToNode[key].Value);
        }

        return default(T);
    }

    public void Remove(string key)
    {
        if (keyToNode.ContainsKey(key))
        {
            DynamicNode target = keyToNode[key];
            if (target == head)
            {
                DynamicNode temp = target.Next;
                target.Next = null;
                if (temp != null)
                {
                    temp.Prev = null;
                }

                head = temp;
            }
            else if (target == tail)
            {
                DynamicNode temp = target.Prev;
                target.Prev = null;
                if (temp != null)
                {
                    temp.Next = null;
                }

                tail = temp;
            }
            else
            {
                DynamicNode p1 = target.Prev;
                DynamicNode p2 = target.Next;
                p1.Next = p2;
                p2.Prev = p1;
                target.Next = null;
                target.Prev = null;
            }

            size -= 1;
            keyToNode.Remove(key);
        }
    }

    public bool ContainsKey(string key)
    {
        return keyToNode.ContainsKey(key);
    }

    public int Size()
    {
        return size;
    }

    public bool HasNext()
    {
        return flagCount < size - 1;
    }

    public KeyValuePair<string, object> Next()
    {
        if (head == null || flagCount == size - 1)
        {
            return new KeyValuePair<string, object>();
        }

        if (flagCount == -1)
        {
            flag = head.Next;
            flagCount += 1;
            return new KeyValuePair<string, object>(head.Key, head.Value);
        }
        else
        {
            DynamicNode dn = flag;
            flag = flag.Next;
            flagCount += 1;
            return new KeyValuePair<string, object>(dn.Key, dn.Value);
        }
    }

    public void Reset()
    {
        flag = null;
        flagCount = -1;
    }

    public object this[string key] => Get(key);

    public IEnumerator<object> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class DynamicNode
{
    public DynamicNode? Next;
    public DynamicNode? Prev;
    public string? Key;
    public object? Value;

    public DynamicNode()
    {
    }

    public DynamicNode(string key, object? value)
    {
        this.Key = key;
        this.Value = value;
    }
}