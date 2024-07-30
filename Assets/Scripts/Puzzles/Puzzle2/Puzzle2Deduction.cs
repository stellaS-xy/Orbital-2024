using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Puzzle2Deduction : MonoBehaviour
{
    public Text dialogueText;

    public Button option1Button;
    public Button option2Button;
    public Button option3Button;

    public ChapterCompletionHandler chapterCompletionHandler;

    private void Start()
    {
        //sceneLoader = FindObjectOfType<SceneLoader>(); // Find the SceneLoader in the scene
        option1Button.onClick.AddListener(() => SelectOption(1));
        option2Button.onClick.AddListener(() => SelectOption(2));
        option3Button.onClick.AddListener(() => SelectOption(3));
        DisplayDialogue("Who possibly stole the cake ?");
    }

    private void SelectOption(int option)
    {
        switch (option)
        {
            case 1:
                DisplayDialogue("His footprints are large, but Hugo prefers cream puffs over cookies! I ordered some for him yesterday, hoping he would enjoy them. But before we start the party, I need to find my birthday cake.");
                break;
            case 2:
                DisplayDialogue("A strong tiger could leave big footprints, but tigers are native to the forest, not the plains. Never heard from him about anything related to the plains. Maybe I should confirm with him later.");
                break;
            case 3:
                DisplayDialogue("Yes, it's Rexa! Why is he breaking into my house? Nothing has been stolen, and he usually doesn't act this way! Even if it was him, why was Finn's fur left in the fridge?");
                chapterCompletionHandler.CompleteChapter();
                SceneTransitionManager.Instance.TransitionToScene(8);
                break;
        }
    }


    private void DisplayDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    

    /*
    private void LoadNextSceneInSequence()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadNextSceneInSequence();
        }
        else
        {
            Debug.LogError("SceneLoader not found in the scene.");
        }
    }
    */
}
