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
                    obj.transform.SetParent(transform);
                    obj.GetComponent<UITemplate>().DataSource = item;
                }
            }
            else
            {
                var obj = Instantiate(Template);
                obj.transform.SetParent(transform);
                obj.GetComponent<UITemplate>().DataSource = DataSource;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
