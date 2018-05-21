using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UITemplate: MonoBehaviour
{
    public object DataSource;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(!Application.isPlaying)
        {
            GetComponentsInChildren<UIBehaviour>().ForEach((ui) =>
            {
                if (!ui.GetComponent<UITemplateComponent>())
                    ui.gameObject.AddComponent<UITemplateComponent>();
            });
        }
    }
}