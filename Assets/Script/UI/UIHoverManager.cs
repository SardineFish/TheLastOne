using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIHoverManager: Singleton<UIHoverManager>
{
    public List<UIHover> UIHoverList = new List<UIHover>();
    public void Register(UIHover ui)
    {
        UIHoverList.Add(ui);
        ui.UIObject.transform.SetParent(transform);
    }
}