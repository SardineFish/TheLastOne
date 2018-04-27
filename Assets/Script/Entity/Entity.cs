using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IMessageReceiver
{
    public const string EVENT_TRIGGER_ENTER = "OnTriggerEnter";
    public const string EVENT_TRIGGER_EXIT = "OnTriggerExit";
    /*public virtual void OnMessage(Message msg)
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<EventBus>()?.Dispatch(EVENT_TRIGGER_ENTER, other);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<EventBus>()?.Dispatch(EVENT_TRIGGER_EXIT, other);
    }
}
