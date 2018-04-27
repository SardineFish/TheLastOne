using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GridEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public static Action<Transform> OnEnter;
    public static Action OnExit;

    //when the mouse enter the grid that has the item
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter tag: " + eventData.pointerEnter.tag);
        if (eventData.pointerEnter.tag == "Item" || (eventData.pointerEnter.tag == "Grid" && transform.childCount!=0))
        {
            if (OnEnter != null)
                OnEnter(transform);
        }
    }
    //when the mouse exit the grid that has the item
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Item" || (eventData.pointerEnter.tag == "Grid" && transform.childCount != 0))
        {
            if (OnExit != null)
                OnExit();
        }
    }
}
