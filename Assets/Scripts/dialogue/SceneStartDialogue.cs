using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SceneStartDialogue : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] startDialogueLines;
    public bool hasName;
    public bool hasDecision;
  
    public string[] choiceContents; // Contents for choices
    public string[] option1Dialogue;
    public string[] option2Dialogue;
    public string[] option3Dialogue;
    public string requiredKeyForOption2;
    public string requiredKeyForOption3;

    public GameObject videoPlayerObject;
    public VideoPlayer videoPlayer;
    public GameObject dialogueBox;
    public Camera videoCamera;

    public GameObject instructionBox;






    private void Start()
    {
        if (DialogueManager.Instance != null)
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(startDialogueLines, hasName));
        }
        if (hasDecision)
        {
            DialogueManager.Instance.OnDialogueEnded += ShowDecision;
        }
        else
        {
            Debug.LogError("DialogueManager instance not found. Ensure you have a DialogueManager in the scene.");
        }

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayerObject.SetActive(false);
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCamera = videoCamera;
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.started += OnVideoStarted;
            videoPlayer.loopPointReached += OnVideoEnded;
        }
    }

    private void ShowDecision()
    {
        Debug.Log("SceneStartDialogue - ShowDecision has been called");
        DialogueManager.Instance.OnDialogueEnded -= ShowDecision;

        if (DecisionManager.Instance == null)
        {
            Debug.LogError("DecisionManager instance not found. Ensure you have a DecisionManager in the scene.");
            return;
        }

        Action[] actions = new Action[choiceContents.Length];
        for (int i = 0; i < choiceContents.Length; i++)
        {
            switch (i)
            {
                case 0:
                    actions[i] = OnOption1Selected;
                    break;
                case 1:
                    if (DecisionManager.Instance.HasKey(requiredKeyForOption2))
                    {
                        actions[i] = OnOption2Selected;
                        
                    }
                    else
                    {
                        actions[i] = null;
                        
                    }
                    break;
                case 2:
                    if (DecisionManager.Instance.HasKey(requiredKeyForOption3))
                    {
                        actions[i] = OnOption3Selected;
                        
                    }
                    else
                    {
                        actions[i] = null;
                        
                    }
                    break;
            }
        }

        DecisionManager.Instance.ShowDecision(choiceContents, actions);
    }

    private void OnOption1Selected()
    {
        Debug.Log("Option 1 selected");
        if (option1Dialogue != null && option1Dialogue.Length > 0)
        {
            StartCoroutine(HandleOption1Dialogue());
        }
    }

    private IEnumerator HandleOption1Dialogue()
    {
        Debug.Log("HandleOption1Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option1Dialogue, false));
        Debug.Log("DialogueManager handled option1 dialogue");

        yield return StartCoroutine(WaitForDialogueToEnd());

        Debug.Log("Starting video playback");
        videoPlayerObject.SetActive(true);
        yield return StartCoroutine(PlayVideo());
        Debug.Log("PlayVideo is called");
        

        

        // After the video is played, mark option 1 as completed and show the decision UI again
        DecisionManager.Instance.CompleteOption1();
        ShowDecision();
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        Debug.Log("Waiting for dialogue to end...");
        while (dialogueBox.activeInHierarchy)
        {
            yield return null;
        }
        Debug.Log("Dialogue ended");
    }

    private IEnumerator PlayVideo()
    {

        Debug.Log("Playing video...");


        videoPlayerObject.SetActive(true); // Enable the video canvas

        videoPlayer.Prepare();
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        instructionBox.SetActive(false);
        videoPlayer.Play(); // Play the video

        Debug.Log("Playing video...");
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        videoPlayerObject.SetActive(false); // Disable the video canvas
        Debug.Log("Video finished.");
        instructionBox.SetActive(true);
    }


    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video is prepared.");
    }


    private void OnVideoStarted(VideoPlayer vp)
    {
        Debug.Log("Video has started.");
    }


    private void OnVideoEnded(VideoPlayer vp)
    {
        Debug.Log("Video has ended.");
    }


    /* Additional log to check if the video is prepared
    videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
    videoPlayer.Prepare();


    Debug.Log("Video finished.");
}



private void VideoPlayer_prepareCompleted(VideoPlayer source)
{
    Debug.Log("Video is prepared and playing.");
    videoPlayer.Play();
}
    */


    private void OnOption2Selected()
    {
        Debug.Log("Option 2 selected");
        if (DecisionManager.Instance.HasKey(requiredKeyForOption2))
        {
            if (option2Dialogue != null && option2Dialogue.Length > 0)
            {
                StartCoroutine(HandleOption2Dialogue());
            }
        }
        else
        {
            Debug.Log("Option 2 is not available yet.");
        }
    }

    private IEnumerator HandleOption2Dialogue()
    {
        Debug.Log("HandleOption2Dialogue being called");
        yield return StartCoroutine(DialogueManager.Instance.ShowDialogue(option2Dialogue, false));
        Debug.Log("DialogueManager handled option2 dialogue");

        // Transition to the next scene in sequence
        LoadNextSceneInSequence();
    }

    private void OnOption3Selected()
    {
        Debug.Log("Option 3 selected");
        if (DecisionManager.Instance.HasKey(requiredKeyForOption3))
        {
            if (option3Dialogue != null && option3Dialogue.Length > 0)
            {
                StartCoroutine(DialogueManager.Instance.ShowDialogue(option3Dialogue, false));
            }
        }
        else
        {
            Debug.Log("Option 3 is not available yet.");
        }
    }

    private void LoadNextSceneInSequence()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Ensure the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No more scenes in build settings to load.");
        }
    }

}

