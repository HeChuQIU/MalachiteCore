using System.Reflection;
using MalachiteCore.Core;

namespace MalachiteCore.EventSystem;

public class EventDispatcher
{
    private readonly Dictionary<string, List<ReceiverInfo>> _subs = new();
    public void Subscribe(string senderId, ItemEventBase receiver, string methodName)
    {
        if (senderId == null || receiver == null || methodName == null)
        {
            return;
        }
        ReceiverInfo ri = new ReceiverInfo(receiver, methodName);
        if (_subs.TryGetValue(senderId, out var sub))
        {
            sub.Add(ri);
        }
        else
        {
            List<ReceiverInfo> list = new();
            list.Add(ri);
            _subs.Add(senderId, list);
        }
    }

    public void Send(string senderId, DynamicObject message)
    {
        if (_subs.TryGetValue(senderId, out var sub))
        {
            for (int i = 0; i < sub.Count; i++)
            {
                try
                {
                    sub[i].Method.Invoke(sub[i].Receiver, new object[] { message });
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
    public ItemEventBase? Receiver;

    public string? MethodName;

    public MethodInfo Method;


    public ReceiverInfo() { }
    public ReceiverInfo(ItemEventBase receiver, string methodName)
    {
        this.Receiver = receiver;
        this.MethodName = methodName;
    }

    public void SetMethodInfo()
    {
        Type type = this.Receiver.GetType();
        MethodInfo method = type.GetMethod(MethodName);
        if (method == null)
        {
            return;
        }
        this.Method = method;
    }
}