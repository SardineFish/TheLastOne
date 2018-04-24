using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
    protected Dictionary<string, List<EventListener>> Listeners = new Dictionary<string, List<EventListener>>();
    public void AddEventListener(string eventName,EventListener listener)
    {
        if (Listeners[eventName] == null)
            Listeners[eventName] = new List<EventListener>();
        Listeners[eventName].Add(listener);

    }
    public void RemoveEventListener(string eventName, EventListener listener)
    {
        if (Listeners.ContainsKey(eventName))
            Listeners[eventName].Remove(listener);
    }
    public void Dispatch(string eventName,params object[] args)
    {
        if(Listeners.ContainsKey(eventName))
        {
            try
            {
                Listeners[eventName].ForEach(listener => listener.Method.Invoke(listener.Object, args));
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
