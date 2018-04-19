using UnityEngine;
using UnityEngine.UI;

public class TextUI : GameUI
{
    public static GameObject UIPrefab;
    public string Text = "Text";
    public GameObject UIObject;

    public override GameObject RenderUI()
    {
        var obj = GameObject.Instantiate(UIPrefab);
        obj.GetComponent<Text>().text = Text;
        return UIObject =  obj;
    }
}