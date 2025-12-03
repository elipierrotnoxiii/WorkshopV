using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "MainMenu";
    public void ChangeScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
