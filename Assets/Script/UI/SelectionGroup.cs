﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.Events;

public class SelectionGroup : EventBus
{
    public int SelectedIndex = -1;
    public SelectionButton Selected = null;
    public List<SelectionButton> SelectionButtons = new List<SelectionButton>();
    public const string EVENT_ON_SELECT_CHANGE = "OnSelectChange";

    void Update()
    {
        var selections = GetComponentsInChildren<SelectionButton>();
        SelectionButtons = selections.ToList();
        for(var i=0;i<selections.Length;i++)
        {
            selections[i].Index = i;
            selections[i].SelectionGroup = this;
        }
    }

    public void OnSelectedCallback(SelectionButton selectionButton)
    {
        SelectionButtons
            .Where(selection => selection != selectionButton)
            .ForEach(selection => selection.Selected = false);
        SelectedIndex = SelectionButtons.IndexOf(selectionButton);
        Selected = selectionButton;
        Dispatch(EVENT_ON_SELECT_CHANGE, selectionButton);
    }
}