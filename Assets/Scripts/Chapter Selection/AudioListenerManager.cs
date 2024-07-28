using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioListenerManager : MonoBehaviour
{
    private void Awake()
    {
        ManageAudioListeners();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ManageAudioListeners();
    }

    private void ManageAudioListeners()
    {
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        if (audioListeners.Length > 1)
        {
            for (int i = 1; i < audioListeners.Length; i++)
            {
                audioListeners[i].enabled = false;
            }
        }
    }
}
