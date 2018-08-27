using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    public SelectionGroup SelectionGroup = null;
    public int Index => SelectionGroup.IndexOf(this);
    public bool Selected => SelectionGroup.Selected == this;

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
        SelectionGroup.Select(this);
    }

    public void Deselect()
    {
        if (Selected)
            SelectionGroup.SelectedIndex = -1;
    }

    public void OnSelectedCallback()
    {
        GetComponent<Animator>().SetBool(SelectAnimatorParam, true);
    }

    public void onDeselectedCallback()
    {
        GetComponent<Animator>().SetBool(SelectAnimatorParam, false);
    }
}
