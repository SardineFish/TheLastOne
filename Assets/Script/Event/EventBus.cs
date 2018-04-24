using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
    protected Dictionary<string, List<Delegate>> Listeners = new Dictionary<string, List<Delegate>>();
    public void AddEventListener(string eventName,Delegate eventListener)
    {
        if (Listeners[eventName] == null)
            Listeners[eventName] = new List<Delegate>();
        Listeners[eventName].Add(eventListener);

    }
    public void RemoveEventListener(string eventName,Delegate eventListener)
    {
        if (Listeners.ContainsKey(eventName))
            Listeners[eventName].Remove(eventListener);
    }
    public void Dispatch(string eventName,params object[] args)
    {
        if(Listeners.ContainsKey(eventName))
        {
            try
            {
                Listeners[eventName].ForEach(listener => listener.DynamicInvoke(args));
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
