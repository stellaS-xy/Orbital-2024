using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance { get; private set; }

    private Dictionary<string, bool> keyStates;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            keyStates = new Dictionary<string, bool>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ObtainKey(string key)
    {
        if (!keyStates.ContainsKey(key))
        {
            keyStates[key] = true;
        }
    }

    public bool HasKey(string key)
    {
        return keyStates.ContainsKey(key) && keyStates[key];
    }
}
