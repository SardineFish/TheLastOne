using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StackPanel : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var children = transform.GetChild(i);
            var trans = children.GetComponent<RectTransform>();
            if (!trans)
                continue;
            
            children.transform.localPosition = Vector3.zero;
        }
    }
}
