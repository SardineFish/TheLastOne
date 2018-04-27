using System.Collections.Generic;
using Assets.Script.Package.Modle;

public static class ItemModel{
    //this name is the grid name
    public static Dictionary<string, BaseItem> GridItem = new Dictionary<string, BaseItem>();

    //store item by grid name
    public static void storeItem(string name, BaseItem item)
    {
        if(!GridItem.ContainsKey(name))
        GridItem.Add(name, item);
    }

    //get item by grid name
    public static BaseItem getItem(string name)
    {
        if (GridItem.ContainsKey(name))
            return GridItem[name];
        else
            return null;
    }

    //delete item by grid name
    public static BaseItem deleteItem(string name)
    {
        if (GridItem.ContainsKey(name))
        {
            BaseItem item = GridItem[name];
            GridItem.Remove(name);
            return item;
        }
        else
            return null;      
    }
}
