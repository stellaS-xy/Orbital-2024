using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartNew: MonoBehaviour
{
    public void OnStartButtonClick()
    {
        StartCoroutine(HandleGameStart());
    }

    private IEnumerator HandleGameStart()
    {
        // Clear PlayerPrefs data
        ClearPlayerPrefs();

        // Wait for a frame to ensure PlayerPrefs is cleared
        yield return null;

        // Load the first scene
        SceneTransitionManager.Instance.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared.");
    }
}
