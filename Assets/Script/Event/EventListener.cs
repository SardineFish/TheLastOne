using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

public class EventListener
{
    public EventListener(string eventName, MethodInfo method, object @object)
    {
        EventName = eventName;
        Method = method;
        Object = @object;
    }

    public string EventName { get; set; }
    public object Object { get; set; }
    public MethodInfo Method { get; set; }

}