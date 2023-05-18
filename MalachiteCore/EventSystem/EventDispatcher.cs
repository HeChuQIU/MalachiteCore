using System.Reflection;
using MalachiteCore.Core;

namespace MalachiteCore.EventSystem;

public class EventDispatcher
{
    private Dictionary<string, List<ReceiverInfo>> subs = new();
    public void Subscribe(string senderId, ItemEventBase receiver, string methodName)
    {
        if (senderId == null || receiver == null || methodName == null)
        {
            return;
        }
        ReceiverInfo ri = new ReceiverInfo(receiver, methodName);
        if (subs.ContainsKey(senderId))
        {
            subs[senderId].Add(ri);
        }
        else
        {
            List<ReceiverInfo> list = new();
            list.Add(ri);
            subs.Add(senderId, list);
        }
    }

    public void Send(string senderId, DynamicObject message)
    {
        if (subs.ContainsKey(senderId))
        {
            List<ReceiverInfo> list = subs[senderId];
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    list[i].method.Invoke(list[i].receiver, new object[] { message });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}


class ReceiverInfo
{
    public ItemEventBase? receiver;

    public string? methodName;

    public MethodInfo method;


    public ReceiverInfo() { }
    public ReceiverInfo(ItemEventBase receiver, string methodName)
    {
        this.receiver = receiver;
        this.methodName = methodName;
    }

    public void SetMethodInfo()
    {
        Type type = this.receiver.GetType();
        MethodInfo method = type.GetMethod(methodName);
        if (method == null)
        {
            return;
        }
        this.method = method;
    }
}