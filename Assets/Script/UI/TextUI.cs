using UnityEngine;
using UnityEngine.UI;

public class TextUI : GameUI
{
    [SerializeField]
    public static GameObject UIPrefab;
    public string Text = "Text";


    public override GameObject RenderUI()
    {
        var obj = GameObject.Instantiate(UIPrefab);
        obj.GetComponent<Text>().text = Text;
        return obj;
    }
}