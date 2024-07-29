using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Puzzle5Manager : MonoBehaviour
{
    [SerializeField] InputField playerAnswerUI;
    [SerializeField] Text hintMessage;
    public int playerAnswer;
    public int correctAnswer = 21;
    private List<string> messages;
    private System.Random random;
    //private SceneLoader sceneLoader;

    

    // Start is called before the first frame update
    void Start()
    {
        // Find the SceneLoader in the scene
        //sceneLoader = FindObjectOfType<SceneLoader>(); 
        UpdateHintMessage("What will be the answer?");
        // Initialize a lists of hint messages
        messages = new List<string>
        {
            "Oops! Wrong answer :/",
            "TRUTH: It seems the given numbers are all below 30...",
            "Try Again!",
            "TRUTH: I think it's almost there...",
            "TRUTH: How about convert the numbers to something else...",
        };
        
        // Initialize the random number generator
        random = new System.Random();
    }

    // Update hint messsage according to user input
    void UpdateHintMessage(string message)
    {
        hintMessage.text = message;
    }

    public void ValidateAnswer()
    {
        // Check if the input is valid
        bool isValidInput = int.TryParse(playerAnswerUI.text, out playerAnswer);
        if (isValidInput)
        {
            if (playerAnswer == correctAnswer) 
            {
                UpdateHintMessage("Congrats!");
                PuzzleManager.Instance.SetPuzzleCompleted("Puzzle5");
                // Scene switch back the main game after completing the puzzle
                SceneTransitionManager.Instance.TransitionToScene(17);
            } 
            else
            {
                // Select a random message from the list
                int index = random.Next(messages.Count);
                string randomMessage = messages[index];
                UpdateHintMessage(randomMessage);
            }
        }
        else
        {
            UpdateHintMessage("Invalid input. Please enter a number.");
        }
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
