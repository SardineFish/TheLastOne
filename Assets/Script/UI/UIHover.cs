using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIHover : MonoBehaviour
{
    public GameObject UIPrefab;
    public List<GameUI> UIList = new List<GameUI>();
    public GameObject UIObject;
    // Use this for initialization
    void Start()
    {
        if (!UIObject)
            UIObject = Instantiate(UIPrefab);
        UIHoverManager.Instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddUI(GameUI ui)
    {
        UIList.Add(ui);
        ui.RenderUI().transform.SetParent(UIObject.transform);
    }

    /*public GameObject RenderUI()
    {
        return UIObject;
    }*/
}
