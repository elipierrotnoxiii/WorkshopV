using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBootstrap : MonoBehaviour
{
    void Awake()
{
    DontDestroyOnLoad(gameObject);
}
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera cam = Camera.main;

        if (cam != null)
        {
            MouseUtil.SetCamera(cam);
        }
        else
        {
            Debug.LogError("CameraBootstrap: No MainCamera found in scene " + scene.name);
        }
    }
}