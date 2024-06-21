using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { FreeRoam, Dialogue, PuzzleSolving }

public class GameController : MonoBehaviour
{ 
    [SerializeField] PlayerController playerController;
    [SerializeField] Button button;

    GameState state;

    private void Start()
    {
        state = GameState.FreeRoam;

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

            if (DialogueManager.Instance.IsDialogueBoxActive())
            {
                state = GameState.Dialogue;
            }
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();

            if (!DialogueManager.Instance.IsDialogueBoxActive())
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

public class ScriptData
{
    public int loadType; //type of resources to load; 1.BG 2.character
    public string characterName;
    public string spriteName; //image resource path;
    public string dialogueContent;

}
