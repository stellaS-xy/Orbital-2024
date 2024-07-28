using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {

    }

    public void GoBackToMenu()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        //TransitionManager.Instance.Transition(currentScene, "Menu");
    }
}
