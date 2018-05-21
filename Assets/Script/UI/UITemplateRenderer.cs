using UnityEngine;
using System.Collections;

public class UITemplateRenderer : MonoBehaviour
{
    public GameObject Template;
    public object DataSource;
    // Use this for initialization
    void Start()
    {
        if (DataSource != null)
        {
            if(DataSource is IList)
            {
                var dataList = DataSource as IList;
                foreach(var item in dataList)
                {
                    var obj = Instantiate(Template);
                    obj.GetComponent<UITemplateComponent>().DataSource = item;
                    obj.transform.SetParent(transform);
                }
            }
            else
            {
                var obj = Instantiate(Template);
                obj.GetComponent<UITemplateComponent>().DataSource = DataSource;
                obj.transform.SetParent(transform);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
