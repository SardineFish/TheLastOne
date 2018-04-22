using UnityEngine;
[ExecuteInEditMode]
public class ValueBar : HoverUIComponent
{
    public GameObject UIPrefab;
    public Range Range = new Range(0, 100);
    public float Value;
    public float marginRight = 0;
    public GameObject UIObject;
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

    public void Update()
    {
        UpdateValue();
    }

    public override GameObject RenderUI()
    {
        var obj = GameObject.Instantiate(UIPrefab);
        
        //bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, borderWidth - width + marginRight,);

        UIObject = obj;
        UpdateValue();
        return obj;
    }

    public void UpdateValue()
    {
        if (Range.length <= 0)
            return;
        var borderWidth = 5;
        var border = UIObject.GetComponent<RectTransform>();
        var bar = UIObject.transform.Find("Bar").GetComponent<RectTransform>();
        //var borderWidth = border.rect.width - bar.rect.xMin - bar.rect.xMax;
        //marginRight = bar.rect.right;
        var width = (border.rect.width - borderWidth) * (Value - Min) / Range.length;
        bar.offsetMax = new Vector2(width, bar.offsetMax.y);
    }
}