using System.Reflection;
using MalachiteCore.EventSystem;

namespace MalachiteCore.Core;

public abstract class ItemEventBase
{
    public EventDispatcher ed;

    public object Call(string methodName, params object[] args)
    {
        if (methodName == null)
        {
            return null;
        }
        Type type = this.GetType();
        MethodInfo method = type.GetMethod(methodName);
        if (method == null)
        {
            return null;
        }
        try
        {
            object result = method.Invoke(this, args);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return null;
    }

    public void Send(string senderId, DynamicObject message)
    {
        ed.Send(senderId, message);
    }

    public ItemEventBase()
    {
    }
    public ItemEventBase(EventDispatcher ed)
    {
        this.ed = ed;
    }
}
