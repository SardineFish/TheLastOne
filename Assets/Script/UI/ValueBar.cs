using UnityEngine;

public class ValueBar : GameUI
{
    public GameObject UIPrefab;
    public Range Range;
    public float Value;
    public float marginRight = 0;
    public float Min 
    {
        get
        {
            return Range.min;
        }
        set
        {
            Range.min = value;
        }
    }
    public float Max
    {
        get
        {
            return Range.max;
        }
        set
        {
            Range.max = value;
        }
    }

    public override GameObject RenderUI()
    {
        var obj = GameObject.Instantiate(UIPrefab);
        var border = obj.GetComponent<RectTransform>();
        var bar = obj.transform.Find("Bar").GetComponent<RectTransform>();
        var borderWidth = border.rect.width - bar.rect.xMin - bar.rect.xMax;
        marginRight = bar.rect.right;
        var width = borderWidth * (Value - Min) / Range.length;
        bar.offsetMax = new Vector2(borderWidth - width + marginRight, bar.offsetMax.y);
        //bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, borderWidth - width + marginRight,);

        return obj;
        
    }
}