using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIHoverManager: Singleton<UIHoverManager>
{
    public List<UIHover> UIHoverList = new List<UIHover>();
    public void Start()
    {
    }
    public void Update()
    {
        
    }
    public void Register(UIHover ui)
    {
        if (!UIHoverList.Contains(ui))
            UIHoverList.Add(ui);
        ui.UIObject.transform.SetParent(transform);
        ui.UIObject.transform.SetSiblingIndex(0);
        //transform.childCount
    }
}