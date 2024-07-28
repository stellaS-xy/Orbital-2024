using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    public string persistentSceneName = "Persistent"; // Name of the persistent scene

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load the persistent scene if not already loaded
            if (!IsSceneLoaded(persistentSceneName))
            {
                SceneManager.LoadScene(persistentSceneName, LoadSceneMode.Additive);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void TransitionToScene(int sceneBuildIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneBuildIndex));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Unload the current scene if it's not the persistent scene
        if (currentScene.name != persistentSceneName)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOperation.isDone)
            {
                yield return null;
            }
        }

        // Load the new scene
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Optionally, you could activate the new scene explicitly if needed
        Scene newScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(newScene);
    }

    private IEnumerator LoadSceneAsync(int sceneBuildIndex)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Unload the current scene if it's not the persistent scene
        if (currentScene.name != persistentSceneName)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOperation.isDone)
            {
                yield return null;
            }
        }

        // Load the new scene
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Optionally, you could activate the new scene explicitly if needed
        Scene newScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
        SceneManager.SetActiveScene(newScene);
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
