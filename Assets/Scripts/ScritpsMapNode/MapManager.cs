using UnityEngine;
using System.Collections.Generic;




public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public GameObject nodePrefab;
    public float xSpacing = 3f;
    public float ySpacing = 2.5f;

    int [] floorLayout = { 1, 2 , 3, 4, 1 };

    private NodeView currentNode;
    private List<List<NodeData>> mapData;
    private Dictionary<NodeData, NodeView> nodeLookup = new();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateMap();
        DrawMap();
        DrawConnections();
    }

    void GenerateMap()
    {
       mapData = new();

    int[] layout = { 1, 2, 2, 1, 1 }; // TOTAL = 7

    for (int i = 0; i < layout.Length; i++)
    {
        var floor = new List<NodeData>();

        for (int j = 0; j < layout[i]; j++)
        {
            NodeType type = NodeType.Combat;

            if (i == 0)
                type = NodeType.Start;
            else if (i == layout.Length - 1)
                type = NodeType.Boss;

            floor.Add(new NodeData { type = type });
        }

        mapData.Add(floor);
    }

    ConnectMap(mapData);
    }

    void ConnectMap(List<List<NodeData>> map)
    {
        for(int i = 0; i < map.Count - 1; i++)
        {
            ConnectFloors(map[i], map[i + 1]);
        }
    }

    void ConnectFloors(List<NodeData> from, List<NodeData> to)
{
    // Paso 1: asegurar que todos los nodos del piso siguiente
    // tengan al menos una conexión
    foreach (var target in to)
    {
        var source = from[Random.Range(0, from.Count)];
        if (!source.nextNodes.Contains(target))
            source.nextNodes.Add(target);
    }

    // Paso 2: agregar conexiones extra (máx 2 por nodo)
    foreach (var source in from)
    {
        if (source.nextNodes.Count >= 2)
            continue;

        if (Random.value < 0.5f)
        {
            var target = to[Random.Range(0, to.Count)];
            if (!source.nextNodes.Contains(target))
                source.nextNodes.Add(target);
        }
    }
}



    List<NodeData> CreateFloor(int count)
    {
       var floor = new List<NodeData>();

       for (int i =0; i < count; i++)
        {
            floor.Add(new NodeData
            {
                type = NodeType.Combat
            });
        }

        return floor;
    }

    void DrawMap()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        nodeLookup.Clear();
        for(int f = 0; f < mapData.Count; f++)
        {
            var floor = mapData[f];
            float startX = -(floor.Count -1) * xSpacing / 2;

            for(int i = 0; i < floor.Count; i++)
            {
               Vector3 pos = new Vector3(-f * xSpacing, startX + i * ySpacing, 0);
               var go = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
               var view = go.GetComponent<NodeView>();
               view.Initialize(floor[i]);
               nodeLookup.Add(floor[i], view);
            }
        }
    }

    void DrawConnections()
    {
        foreach(var floor in mapData)
        {
            foreach(var node in floor)
            {
               foreach(var target in node.nextNodes)
                {
                    DrawLine(node, target);
                }
            }
        }
    }

    void DrawLine(NodeData from, NodeData to)
    {
        var lineObj = new GameObject("Connection");
        lineObj.transform.parent = transform;

        var lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        lr.SetPosition(0, nodeLookup[from].transform.position);
        lr.SetPosition(1, nodeLookup[to].transform.position);

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
    }

    public void SelectNode(NodeView node)
    {
        if (currentNode != null)
        {
            if (!currentNode.data.nextNodes.Contains(node.data))
                return;

            currentNode.SetState(NodeState.Visited);
        }

        currentNode = node;
        node.SetState(NodeState.Current);

        foreach (var next in node.data.nextNodes)
        {
            nodeLookup[next].SetState(NodeState.Available);
        }
    }
}