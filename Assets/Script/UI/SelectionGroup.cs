using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.Events;

public class SelectionGroup : EventBus
{
    private int selectedIndex = -1;
    public int SelectedIndex
    {
        get { return selectedIndex; }
        set { Select(value); }
    }

    public SelectionButton Selected
    {
        get
        {
            if (SelectedIndex < 0)
                return null;
            return SelectionButtons[SelectedIndex];
        }
        set
        {
            Select(value);
        }
    }
    public List<SelectionButton> SelectionButtons = new List<SelectionButton>();
    public const string EVENT_ON_SELECT_CHANGE = "OnSelectChange";

    void Update()
    {
        var selections = GetComponentsInChildren<SelectionButton>();
        SelectionButtons = selections.ToList();
        for(var i=0;i<selections.Length;i++)
        {
            selections[i].SelectionGroup = this;
        }

    }

    public void ClearSelection()
    {
        Select(-1);
    }

    public int IndexOf(SelectionButton selectionButton)
    {
        return SelectionButtons.IndexOf(selectionButton);
    }

    public void Select(SelectionButton selectionButton)
    {
        Select(IndexOf(selectionButton));
    }

    public void Select(int idx)
    {
        if (idx == selectedIndex)
            return;
        if (idx >= SelectionButtons.Count)
        {
            throw new IndexOutOfRangeException();
        }
        Selected?.onDeselectedCallback();
        selectedIndex = idx;
        if (idx < 0)
            return;
        Selected?.OnSelectedCallback();
        Dispatch(EVENT_ON_SELECT_CHANGE, SelectionButtons[idx]);
    }
}