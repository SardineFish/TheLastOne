using UnityEngine;
using UnityEngine.UI;

public class TextUI : HoverUIComponent
{
    public GameObject UIPrefab;
    public string Text = "Text";
    public GameObject UIObject;

    public override GameObject RenderUI()
    {
        var obj = GameObject.Instantiate(UIPrefab);
        obj.GetComponent<Text>().text = Text;
        return UIObject =  obj;
    }
}