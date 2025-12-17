using UnityEngine;

public class NodeView : MonoBehaviour
{
    public NodeData data;
    public NodeState currentState;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Initialize(NodeData nodeData)
    {
       data = nodeData;

    if (data.type == NodeType.Start)
        SetState(NodeState.Available);
    else
        SetState(NodeState.Locked);
    }

    public void SetState(NodeState state)
{   
    currentState = state;

        // No cambiar color para START

     if (data.type == NodeType.Start && state == NodeState.Locked)
            return;

        switch (state)
        {
             case NodeState.Current:
            rend.material.color = Color.yellow; // todos Current, incluido Heal
            break;

        case NodeState.Available:
            if (data.type == NodeType.Heal)
                rend.material.color = Color.cyan; // Heal disponible
            else
                rend.material.color = Color.white; // Combat disponible
            break;

        case NodeState.Visited:
            rend.material.color = Color.green;
            break;

        case NodeState.Locked:
            rend.material.color = Color.gray;
            break;

        case NodeState.Blocked:
            rend.material.color = Color.black;
            break;
        }
}



    void OnMouseDown()
    {
        if (currentState != NodeState.Available &&
        currentState != NodeState.Current)
            return;

        if (MapManager.Instance == null)
            return;

        MapManager.Instance.SelectNode(this);
    }

}

public enum NodeState
{
   Locked,      // No se puede interactuar
    Available,   // Se puede elegir
    Current,     // Donde está el jugador
    Visited,     // Ya fue completado
    Blocked      // Inaccesible por ruta (opcional)
}  