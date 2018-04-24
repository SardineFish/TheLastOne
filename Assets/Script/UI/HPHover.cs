using UnityEngine;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(LifeBody),typeof(UIHover))]
public class HPHover : HoverUIComponent
{
    public GameObject UIPrefab;
    public Range Range = new Range(0, 100);
    public float Value;
    float marginRight = 0;

    //[NonSerialized]
    public GameObject UIObject = null;
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

    [ExecuteInEditMode]
    void Start()
    {
        Min = 0;
        Max = (Entity as LifeBody).MaxHP;
        Entity.GetComponent<UIHover>()?.AddUI(this);
    }
    [ExecuteInEditMode]
    public void Update()
    {
        Value = (Entity as LifeBody).HP;
        Max = (Entity as LifeBody).MaxHP;
        UpdateValue();
    }

    public override GameObject RenderUI()
    {
        if (UIObject)
        {
            UpdateValue();
            return UIObject;
        }
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