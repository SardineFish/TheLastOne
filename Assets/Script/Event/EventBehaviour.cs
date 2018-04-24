using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;

public class EventBehaviour : MonoBehaviour
{
    public EventBus EventTarget { get; set; }
    protected Delegate[] EventListeners;
    public EventBehaviour() : base()
    {

    }

    public EventBehaviour(EventBus eventBus) : this()
    {
        Bind(eventBus);
    }

    public void Bind(EventBus eventBus)
    {
        if(EventListeners == null)
        {
            EventListeners = GetType()
                .GetMethods()
                .Where(method => method.GetCustomAttributes<EventListenerAttribute>().FirstOrDefault() != null)
                .Select(method => Delegate.CreateDelegate(GetType(), method))
                .ToArray();
        }

        if(EventTarget)
        {
            foreach(var listener in EventListeners)
            {
                EventTarget.RemoveEventListener(listener.Method.GetCustomAttribute<EventListenerAttribute>().EventName, listener);
            }
        }

        EventTarget = eventBus;
        foreach(var listener in EventListeners)
        {
            EventTarget.AddEventListener(listener.Method.GetCustomAttribute<EventListenerAttribute>().EventName, listener);
        }

    }
}
