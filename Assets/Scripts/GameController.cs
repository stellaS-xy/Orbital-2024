using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { FreeRoam, Dialogue, PuzzleSolving, DecisionMaking}

public class GameController : MonoBehaviour
{ 
    [SerializeField] PlayerController playerController;
    [SerializeField] Button button;
    public GameObject DecisionBoxUI;
    public GameObject DialogueBoxUI;

    GameState state;

    private void Start()
    {
        state = GameState.FreeRoam;
        
        // Mark this chapter as started
        //ChapterManager.Instance.SetChapterStarted("Chapter 1-1");

    }
    
        /*
        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };
        DialogueManager.Instance.OnHideDialogue += () =>
        {
            if (state == GameState.Dialogue)
                state = GameState.FreeRoam;
        };
        
    }
    */

    private void Update() 
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            if (DecisionBoxUI.activeInHierarchy | DialogueBoxUI.activeInHierarchy)
            {
                state = GameState.Dialogue;
            }

          
        }

        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();

            if (!DecisionBoxUI.activeInHierarchy && !DialogueBoxUI.activeInHierarchy)
            {
                state = GameState.FreeRoam;
            }

            

        }


       
            
    }
}

/*
        }  else if (state == GameState.PuzzleSolving)
        {

        }
    }
    
}
*/

/*
public class ScriptData
{
    public int loadType; //type of resources to load; 1.BG 2.character
    public string characterName;
    public string spriteName; //image resource path;
    public string dialogueContent;

}
*/
