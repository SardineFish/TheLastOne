using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
    private readonly Dictionary<string, List<EventListenerBase>> Listeners = new Dictionary<string, List<EventListenerBase>>();

    public EventBus()
    {
        Listeners = new Dictionary<string, List<EventListenerBase>>();
    }
    public void AddEventListener(string eventName,ReflectEventListener listener)
    {
        Debug.Log(gameObject);
        Debug.Log(Listeners);
        if (!Listeners.ContainsKey(eventName))
            Listeners[eventName] = new List<EventListenerBase>();
        Listeners[eventName].Add(listener);

    }
    public void AddEventListener<T>(string eventName, Action<T> listener)
    {
        Debug.Log(gameObject);
        if (!Listeners.ContainsKey(eventName))
            Listeners[eventName] = new List<EventListenerBase>();
        Listeners[eventName].Add(new ActionEventListener<T>(listener));
    }
    public void AddEventListener(string eventName, Action listener)
    {
        Debug.Log(gameObject);
        AddEventListener<object>(eventName, (obj) => listener());
    }
    public void RemoveEventListener(string eventName, EventListenerBase listener)
    {
        if (Listeners.ContainsKey(eventName))
            Listeners[eventName].Remove(listener);
    }
    public void Dispatch(string eventName,params object[] args)
    {
        Debug.Log(gameObject);
        Debug.Log(Listeners);
        if(Listeners.ContainsKey(eventName))
        {
            Listeners[eventName].ForEach(listener => listener.Invoke(args));
            //Listeners[eventName].ForEach(listener => listener.Method.Invoke(listener.Object, args));
            /*try
            {
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }*/
        }
    }
}
