using MalachiteCore.Core;
namespace MalachiteCore.Core;

public class DynamicObject
{
    DynamicNode? head;
    DynamicNode? tail;
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


    public void put(string key, object value)
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

    public object get(string key)
    {
        if (keyToNode.ContainsKey(key))
        {
            return keyToNode[key].Value;
        }
        return null;
    }

    public void remove(string key)
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