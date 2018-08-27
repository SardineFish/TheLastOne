using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIHover : EntityBehavior<Entity>
{
    public GameObject UIPrefab = null;
    public List<HoverUIComponent> UIList = new List<HoverUIComponent>();
    //[NonSerialized]
    public GameObject UIObject = null;
    public Vector3 UIPosition;
    
    // Use this for initialization
    void Start()
    {
        Utility.NextFrame(this, () =>
        {
            if (!UIObject)
            {
                UIObject = Instantiate(UIPrefab);
                UIHoverManager.Instance.Register(this);
                UIObject.GetComponent<Billboard>().BindTarget = Entity.gameObject;
                UIObject.GetComponent<Billboard>().RelativePosition = UIPosition;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIObject)
        {
            UIObject = Instantiate(UIPrefab);
            UIHoverManager.Instance.Register(this);
            UIObject.GetComponent<Billboard>().BindTarget = gameObject;
            UIObject.GetComponent<Billboard>().RelativePosition = UIPosition;
        }
        for (var i = 0; i < UIList.Count;i++)
        {
            if(!UIList[i])
                UIList.RemoveAt(i--);
        }
        UIObject.GetComponent<Billboard>().RelativePosition = UIPosition;
    }

    private void OnDestroy()
    {
        GameObject.DestroyImmediate(UIObject);
    }



    public void AddUI(HoverUIComponent ui)
    {
        if (!UIObject)
        {
            UIObject = Instantiate(UIPrefab);
            UIHoverManager.Instance.Register(this);
            UIObject.GetComponent<Billboard>().BindTarget = gameObject;
            UIObject.GetComponent<Billboard>().RelativePosition = UIPosition;
        }
        if(!UIList.Contains(ui))
            UIList.Add(ui);
        ui.RenderUI().transform.SetParent(UIObject.transform);
    }

    /*public GameObject RenderUI()
    {
        return UIObject;
    }*/
}
