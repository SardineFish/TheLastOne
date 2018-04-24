using UnityEngine;
using System.Collections;

public class InteractSkill : AnimationSkill
{

    void Start()
    {
        UseEventListener();
    }
    public override void Update()
    {

    }

    [EventListener(Entity.EVENT_TRIGGER_ENTER)]
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "In");
    }

    [EventListener(Entity.EVENT_TRIGGER_EXIT)]
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + "Out");
    }
}
