using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Equipments : EntityBehavior<LifeBody>
{
    public GameObject Selected;
    public int SelectedIndex
    {
        get { return Items.IndexOf(Selected); }
        set
        {
            if (value < 0 || value >= Items.Count)
                return;
            Selected = Items[value];
        }
    }
    public List<GameObject> Items = new List<GameObject>();

    GameObject weaponToDraw;
    public void Switch()
    {
        var actionManager = Entity.GetComponent<PlayerActionManager>();
        if (Selected && !actionManager.ChangeAction(actionManager.SheathSword.GetAction(Selected)))
        {
            return;
        }
        weaponToDraw = Selected = Items[(Items.IndexOf(Selected) + 1) % Items.Count];
    }

    public void OnEquip()
    {
        var equipment = Utility.Instantiate(Selected, Entity.gameObject.scene);
        equipment.GetComponent<CarriableObject>().AttachTo(Entity.PrimaryHand);
    }

    public void OnUnequip()
    {
        Entity.PrimaryHand.Release();
    }

    public void OnEndEquip()
    {

    }

    public void OnEndUnequip()
    {
        var actionManager = Entity.GetComponent<PlayerActionManager>();
        if (weaponToDraw)
        {
            if (!actionManager.ChangeAction(actionManager.DrawSword.GetAction(weaponToDraw)))
            {
                this.NextFrame(OnEndUnequip);
                return;
            }
            Selected = weaponToDraw;
            weaponToDraw = null;
        }
    }
}
