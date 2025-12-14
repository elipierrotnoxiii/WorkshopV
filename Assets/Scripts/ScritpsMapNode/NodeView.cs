using UnityEngine;

public class NodeView : MonoBehaviour
{
    public NodeData data;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Initialize(NodeData nodeData)
    {
       data = nodeData;

        // Color especial para START
        if (data.type == NodeType.Start)
        {
            rend.material.color = Color.cyan;
        }
        else
        {
            rend.material.color = Color.gray;
        }
    }

    public void SetState(NodeState state)
{
     if (data.type == NodeType.Start)
            return;

        switch (state)
        {
            case NodeState.Current:
                rend.material.color = Color.yellow;
                break;
            case NodeState.Available:
                rend.material.color = Color.white;
                break;
            case NodeState.Visited:
                rend.material.color = Color.green;
                break;
            case NodeState.Locked:
                rend.material.color = Color.gray;
                break;
        }
}



    void OnMouseDown()
    {
        Debug.Log("Node clicked: " + data.type);
        MapManager.Instance.SelectNode(this);
    }

}

public enum NodeState
{
    Current,
    Available,
    Locked,
    Visited
}  