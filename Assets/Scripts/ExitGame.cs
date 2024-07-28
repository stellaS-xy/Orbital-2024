using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitGame : MonoBehaviour
{
    public void OnExitGame()
    {
        Debug.Log("Return back to main page");
        SceneManager.LoadScene(0);
    }
}
