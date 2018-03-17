using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Message
{
    public virtual void Dispatch(Entity receiver)
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