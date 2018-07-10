using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Equipments : MonoBehaviour
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
    public void Switch()
    {
        Selected = Items[(Items.IndexOf(Selected) + 1) % Items.Count];
    }
}
