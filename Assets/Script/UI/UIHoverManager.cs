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
    //public List<UIHover> UIHoverList = new List<UIHover>();
    public Dictionary<UIHover, GameObject> UIHoverList = new Dictionary<UIHover, GameObject>();
    public void Start()
    {
        for(var i=0;i<transform.childCount;i++)
        {
            var obj = transform.GetChild(i).gameObject;
            if (!UIHoverList.ContainsValue(obj))
            {
                try
                {
                    Destroy(obj);
                }
                catch
                {
                    DestroyImmediate(obj);
                    //i--;
                }
            }
        }
    }
    public void Update()
    {
    }
    public void Register(UIHover ui)
    {
        if (!UIHoverList.ContainsKey(ui))
            UIHoverList[ui] = ui.UIObject;
        else
        {
            Destroy(UIHoverList[ui]);
            UIHoverList[ui] = ui.UIObject;
        }
        ui.UIObject.transform.SetParent(transform);
        ui.UIObject.transform.SetSiblingIndex(0);
        //transform.childCount
    }
}