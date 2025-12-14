using System.Collections.Generic;

[System.Serializable]

public class NodeData
{
    public NodeType type;
    public List<NodeData> nextNodes = new();
    public bool visited;
}