using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlowController : MonoBehaviour
{
    public static BattleFlowController Instance;

    public NodeData currentCombatNode;
    public BattleResult lastResult = BattleResult.None;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartCombat(NodeData node)
    {
        currentCombatNode = node;
        lastResult = BattleResult.None;
        MapManager.Instance.SetMapVisible(false);
        SceneManager.LoadScene("GameScene");
    }

    public void EndCombat(BattleResult result)
    {
        lastResult = result;
        SceneManager.LoadScene("NodeScene");
    }
}