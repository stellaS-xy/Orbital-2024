using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneLoader : MonoBehaviour
{
    private static bool isPersistentSceneLoaded = false;

    void Awake()
    {
        if (!isPersistentSceneLoaded)
        {
            SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
            isPersistentSceneLoaded = true;
        }
    }
}
