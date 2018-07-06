using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectMessage : Message
{
    public SelectionButton Selection { get; private set; }
    public SelectionGroup SelectionGroup { get; private set; }
    public SelectMessage(Entity sender,SelectionButton selectionButton,SelectionGroup selectionGroup) : base(sender)
    {
        Selection = selectionButton;
        SelectionGroup = selectionGroup;
    }

}