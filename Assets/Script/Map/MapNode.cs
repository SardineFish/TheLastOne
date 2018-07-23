using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MapNode
{
    public MapNodeType Type;
    public static MapNode Null = new MapNode() { Type = MapNodeType.None };
}

public enum MapNodeType
{
    None,
    Empty,
    Obstacle,
    Wall,
}