using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionButton : EventBus
{
    public SelectionGroup SelectionGroup = null;
    public int Index = 0;
    public bool Selected = false;

    public string SelectAnimatorParam = "Selected";

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        SelectionGroup?.OnSelectedCallback(this);
        Selected = true;
        GetComponent<Animator>().SetBool(SelectAnimatorParam, true);
    }

    public void Deselect()
    {
        Selected = false;
        GetComponent<Animator>().SetBool(SelectAnimatorParam, false);
    }
}
