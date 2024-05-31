using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue, PuzzleSolving }

public class GameController : MonoBehaviour
{ 
    [SerializeField] PlayerController playerController;

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