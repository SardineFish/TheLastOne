using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Equipments : MonoBehaviour
{
    public GameObject Selected;
    public List<GameObject> Items = new List<GameObject>();
    public void Switch()
    {
        Selected = Items[(Items.IndexOf(Selected) + 1) % Items.Count];
    }
}
