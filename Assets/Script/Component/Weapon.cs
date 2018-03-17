using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float PhysicalDamage;
    public float MagicDamage;
    public bool DamageOnce = true;

    protected List<Entity> damagedEntities = new List<Entity>();

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        damagedEntities.Clear();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        var entity = other.GetComponent<Entity>();

        if (!entity)
            return;

        if (DamageOnce && damagedEntities.Contains(entity))
            return;

        
    }
}
