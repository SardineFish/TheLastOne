using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UITemplateComponent : MonoBehaviour
{
    object dataSource;
    public object DataSource
    {
        get
        {
            return dataSource;
        }
        set
        {
            var old = dataSource;
            dataSource = value;
            if (old != value)
                Reload();
        }
    }
    public List<BindingOption> Bindings = new List<BindingOption>();
    // Use this for initialization
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var bind in Bindings)
        {
            if(bind.BindingMode == BindingMode.OneWay)
            {
                UITemplateUtility.SetValueByPath(gameObject, bind.PathTemplate, UITemplateUtility.GetValueByPath(dataSource, bind.PathSource));
            }
        }
    }

    void Reload()
    {
        foreach (var bind in Bindings)
        {
            UITemplateUtility.SetValueByPath(gameObject, bind.PathTemplate, UITemplateUtility.GetValueByPath(dataSource, bind.PathSource));
        }
    }
}