using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapNode
{
    public MapNodeType Type;
    public static MapNode Null = new MapNode() { Type = MapNodeType.None };
    public Vector3 Center;
}

public enum MapNodeType
{
    None,
    Empty,
    Obstacle,
    Wall,
}