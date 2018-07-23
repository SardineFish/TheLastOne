using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour {
    public float Width = 32;
    public float Height = 32;
    public float NodeSize = 5f;
    public Orientation InDoor = Orientation.None;
    [HideInInspector]
    public bool[] Doors = new bool[4];
    public bool NorthDoor => Doors[0];
    public bool SouthDoor => Doors[1];
    public bool EastDoor => Doors[2];
    public bool WestDoor => Doors[3];
    public float DoorProbability = .5f;

    int mapOffsetX = 0;
    int mapOffsetY = 0;
    int mapSizeX = 0;
    int mapSizeY = 0;
    MapNode[,] map = new MapNode[0, 0];
    public MapNode this[int x,int y]
    {
        get
        {
            x = x + mapOffsetX;
            y = y + mapOffsetY;
            if (x < 0 || y < 0 || x >= mapSizeX || y >= mapSizeY)
                return MapNode.Null;
            return map[x, y];
        }
    }
    public MapNode this[Vector2Int pos] => this[pos.x, pos.y];

    [HideInInspector]
    public WeightedList Walls = new WeightedList();
    [HideInInspector]
    public WeightedList WallsWithDoor = new WeightedList();
    [HideInInspector]
    public WeightedList Obstacles = new WeightedList();

    public GameObject GroundPrefab;
    public Material GroundMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate()
    {
        gameObject.GetChildren().ForEach(child => Destroy(child));
        // Generate ground
        var ground = Instantiate(GroundPrefab, Vector3.zero, Quaternion.identity);
        ground.transform.localScale = new Vector3(Width / 10, 1, Height / 10);
        SceneManager.MoveGameObjectToScene(ground, gameObject.scene);
        ground.transform.parent = transform;

        // Generate doors
        for(var i = 0; i < 4; i++)
        {
            Doors[i] = false;
            if (Random.value < DoorProbability)
            {
                Doors[i] = true;
            }
        }
        if (InDoor != Orientation.None)
            Doors[(int)InDoor] = true;

        var wallNorth = GenerateWall(NorthDoor, Width);
        wallNorth.transform.parent = gameObject.transform;
        wallNorth.transform.position = new Vector3(0, 0, Height / 2);
        wallNorth.transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.forward);

        var wallSouth = GenerateWall(SouthDoor, Width);
        wallSouth.transform.parent = gameObject.transform;
        wallSouth.transform.position = new Vector3(0, 0, -Height / 2);
        wallSouth.transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.back);

        var wallEast = GenerateWall(EastDoor, Height);
        wallEast.transform.parent = gameObject.transform;
        wallEast.transform.position = new Vector3(Width / 2, 0, 0);
        wallEast.transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.right);

        var wallWest = GenerateWall(WestDoor, Height);
        wallWest.transform.parent = gameObject.transform;
        wallWest.transform.position = new Vector3(-Width / 2, 0, 0);
        wallWest.transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.left);

        // Generate map nodes
        mapSizeX = Mathf.CeilToInt((Width / 2 - NodeSize / 2) / NodeSize) * 2 + 1;
        mapOffsetX = mapSizeX / 2;
        mapSizeY = Mathf.CeilToInt((Height / 2 - NodeSize / 2) / NodeSize) * 2 + 1;
        mapOffsetY = mapSizeY / 2;
        map = new MapNode[mapSizeX, mapSizeY];

        
    }

    public Vector2Int ToMapNodeCoordinate(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt((position.x + NodeSize / 2) / NodeSize),
            Mathf.FloorToInt((position.y + NodeSize / 2) / NodeSize)
            );
    }

    public Vector2Int ToMapNodeCoordinate(Vector3 position)
    {
        return ToMapNodeCoordinate(position.ToVector2XZ());
    }

    public GameObject GenerateWall(bool generateDoor, float length)
    {
        var wallObj = new GameObject();
        SceneManager.MoveGameObjectToScene(wallObj, gameObject.scene);
        Wall mainWall;
        if (generateDoor)
            mainWall = (WallsWithDoor.RandomTake(1).FirstOrDefault() as GameObject).GetComponent<Wall>();
        else
            mainWall = (Walls.RandomTake(1).FirstOrDefault() as GameObject).GetComponent<Wall>();
        if (!mainWall)
            throw new System.Exception("No enough walls.");
        mainWall = Utility.Instantiate(mainWall.gameObject, wallObj).GetComponent<Wall>();

        // Put the main wall to an appropriate position
        if (mainWall.Length > length)
            mainWall.transform.position = Vector3.zero;
        else
        {
            var range = length - mainWall.Length;
            var pos = Random.Range(-range / 2, range / 2);
            mainWall.transform.position = new Vector3(0, 0, pos);

            // Fill the rest space
            // Left space
            var restSpace = length / 2 - mainWall.Length / 2 + pos;
            Wall wall;
            AgainLeft:
            wall = Utility.Instantiate(Walls.RandomTake(1).FirstOrDefault() as GameObject, wallObj).GetComponent<Wall>();
            wall.transform.localPosition = new Vector3(0, 0, -length / 2 + restSpace - (wall.Length / 2));
            restSpace -= wall.Length;
            if (restSpace > 0)
                goto AgainLeft;

            restSpace = length / 2 - mainWall.Length / 2 - pos;
            AgainRight:
            wall = Utility.Instantiate(Walls.RandomTake(1).FirstOrDefault() as GameObject, wallObj).GetComponent<Wall>();
            wall.transform.localPosition = new Vector3(0, 0, length / 2 - restSpace + (wall.Length / 2));
            restSpace -= wall.Length;
            if (restSpace > 0)
                goto AgainRight;
        }

        return wallObj;
    }
}

public enum Orientation : int
{
    None = -1,
    North = 0,
    South = 1,
    East = 2,
    West = 3
}