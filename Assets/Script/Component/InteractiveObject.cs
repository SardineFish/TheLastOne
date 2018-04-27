using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TextHover))]
public class InteractiveObject : EventBus, IMessageReceiver
{
    public string TextHover = "Press [" + PlayerInputManager.Instance.InteractKey.ToString() + "]";
    public const string EVENT_ON_INTERACT = "OnInteract";
    public virtual void OnMessage(InteractMessage msg)
    {
        Dispatch(EVENT_ON_INTERACT, msg.Sender);
    }

    void Update()
    {
        GetComponent<TextHover>()?.SetText(TextHover);
    }
}