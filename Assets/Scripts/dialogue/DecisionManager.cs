using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance { get; private set; }

    public GameObject choiceButtonGroup; //parentObject
    public GameObject[] choiceUIGos;
    public Text[] textChoiceUIs;
    public int choiceNum;
    public string[] choiceContent;
    private Action[] choiceActions;
    private Dictionary<string, bool> keyStates;
    public Button[] choiceButtons;
    private bool option1Completed;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            keyStates = new Dictionary<string, bool>();
            option1Completed = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //show the decision making UI
    //choiceNum: number of options
    
    public void ShowDecision(string[] choiceContents, Action[] actions)
    {
        
        choiceButtonGroup.SetActive(true);
        choiceActions = actions;


        for (int i = 0; i < choiceUIGos.Length; i++)
        {
            if (i < choiceContents.Length)
            {
                choiceUIGos[i].SetActive(true);
                textChoiceUIs[i].text = choiceContents[i];
                int index = i; // Capture index for the listener
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].interactable = actions[i] != null; // Disable button if action is null
                if (actions[i] != null)
                {
                    choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
                }
            }
            else
            {
                choiceUIGos[i].SetActive(false);
            }
        }
    }


    


    private void OnChoiceSelected(int index)
    {
        choiceButtonGroup.SetActive(false);
        choiceActions[index]?.Invoke();
    }

    public void CloseChoiceUI()
    {
        choiceButtonGroup.SetActive(false);
    }

    public bool IsDecisionBoxActive()
    {
        return choiceButtonGroup.activeInHierarchy;
    }

    public void ObtainKey(string key)
    {
        keyStates[key] = true;
    }

    public bool HasKey(string key)
    {
        if (keyStates == null)
        {
            Debug.LogError("keyStates dictionary is null.");
            return false;
        }

        if (!keyStates.ContainsKey(key))
        {
            Debug.Log("DecisionManager:Key is not obtained.");
            return false;
        }


        return keyStates[key];
    }

    public bool IsOption1Completed()
    {
        return option1Completed;
    }

    public void CompleteOption1()
    {
        option1Completed = true;
        DecisionManager.Instance.ObtainKey("KEY01");
    }


}