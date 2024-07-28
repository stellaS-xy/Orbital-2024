using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string persistentSceneName = "Persistent"; // Name of the persistent scene

    public void LoadNextSceneInSequence()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

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

        // Ensure the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);
            while (!loadOperation.isDone)
            {
                yield return null;
            }

            // Optionally, you could activate the new scene explicitly if needed
            Scene newScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
            SceneManager.SetActiveScene(newScene);
        }
        else
        {
            Debug.LogError("No more scenes in build settings to load.");
        }
    }
}
