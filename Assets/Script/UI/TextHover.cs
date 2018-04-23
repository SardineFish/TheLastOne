using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(UIHover))]
public class TextHover : HoverUIComponent
{
    public GameObject UIPrefab;
    public string Text = "Text";
    public GameObject UIObject;
    private void Start()
    {
        Entity.GetComponent<UIHover>()?.AddUI(this);
    }
    public void Update()
    {
        UIObject.GetComponent<Text>().text = Text;
    }
    public override GameObject RenderUI()
    {
        if(UIObject)
        {
            return UIObject;
        }
        var obj = GameObject.Instantiate(UIPrefab);
        obj.GetComponent<Text>().text = Text;
        return UIObject =  obj;
    }
}