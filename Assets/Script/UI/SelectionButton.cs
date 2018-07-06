using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionButton : EventBus
{
    public SelectionGroup SelectionGroup = null;
    public int Index = 0;
    public bool Selected = false;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SelectionGroup?.OnSelectedCallback(this);
            Selected = true;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
