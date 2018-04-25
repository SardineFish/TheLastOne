using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractSkill : AnimationSkill
{
    List<InteractiveObject> objInRange = new List<InteractiveObject>();

    void Start()
    {
        this.UseEventListener();
    }
    public override void Update()
    {

    }

    [EventListener(Entity.EVENT_TRIGGER_ENTER)]
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractiveObject>())
            objInRange.Add(other.GetComponent<InteractiveObject>());
    }

    [EventListener(Entity.EVENT_TRIGGER_EXIT)]
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractiveObject>())
            objInRange.Remove(other.GetComponent<InteractiveObject>());
    }

    public override bool Activate()
    {
        return Activate(objInRange.OrderBy(obj => (obj.transform.position - Entity.transform.position).sqrMagnitude).FirstOrDefault());
    }

    public override bool Activate(Vector3 target)
    {
        return Activate();
    }

    public override bool Activate(Entity target)
    {
        return target.GetComponent<InteractiveObject>();
    }

    public virtual bool Activate(InteractiveObject target)
    {
        if (!target)
            return false;
        new InteractMessage(Entity).Dispatch(target);
        return true;
    }
}
