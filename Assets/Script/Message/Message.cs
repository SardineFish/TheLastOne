using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Message
{
    public Message(Entity sender)
    {
        this.Sender = sender;
    }

    public Entity Sender { get; protected set; }

    public virtual void Dispatch(IMessageReceiver receiver)
    {
        receiver.OnMessage(this);
    }

    public virtual void Broadcast(Entity[] receivers)
    {
        foreach(var entity in receivers)
        {
            entity.OnMessage(this);
        }
    }
}