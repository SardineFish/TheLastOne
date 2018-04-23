using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UIHover : EntityBehavior<Entity>
{
    public GameObject UIPrefab;
    public List<HoverUIComponent> UIList = new List<HoverUIComponent>();
    public GameObject UIObject = null;
    public Transform UIPosition;
    
    // Use this for initialization
    void Start()
    {
        if (!UIObject)
        {
            UIObject = Instantiate(UIPrefab);
            UIHoverManager.Instance.Register(this);
            UIObject.GetComponent<Billboard>().BindTarget = Entity.gameObject;
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddUI(HoverUIComponent ui)
    {
        if (!UIObject)
        {
            UIObject = Instantiate(UIPrefab);
            UIHoverManager.Instance.Register(this);
            UIObject.GetComponent<Billboard>().BindTarget = Entity.gameObject;
        }
        UIList.Add(ui);
        ui.RenderUI().transform.SetParent(UIObject.transform);
    }

    /*public GameObject RenderUI()
    {
        return UIObject;
    }*/
}
