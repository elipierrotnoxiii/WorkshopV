using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanelManager : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "GameScene";

    [Header("Assign tutorial panels in order")]
    public GameObject[] panels;

    private int currentIndex = 0;

    private void Start()
    {
        // Hide all panels first
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);

        // Show the first panel (if any)
        if (panels.Length > 0)
            panels[0].SetActive(true);
    }

    public void NextPanel()
    {
        if (currentIndex < panels.Length - 1)
        {
            panels[currentIndex].SetActive(false);
            currentIndex++;
            panels[currentIndex].SetActive(true);
        }
        else
        {
            // Last panel reached => close tutorial
            //panels[currentIndex].SetActive(false);
            SceneManager.LoadScene(gameSceneName);

            // Optional: disable this whole object
            // gameObject.SetActive(false);
        }
    }

    public void PreviousPanel()
    {
        if (currentIndex > 0)
        {
            panels[currentIndex].SetActive(false);
            currentIndex--;
            panels[currentIndex].SetActive(true);
        }
    }
}

