using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] lines;
    [SerializeField] public bool hasName;
    public Sprite faceImage;

    public GameObject TalkButton;
    public bool Interactable;
    public bool hasButtonInitially;

   
    //public bool hasAnimationToPlay;
    

    private void Start()
    {
        TalkButton.SetActive(hasButtonInitially);
        
        // Optional: Subscribe to DialogueManager's OnDialogueEnded event if you want all NPCs to potentially trigger an animation
        //DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
    }

    public void Interact()
    {
        if (Interactable)
        {
            Debug.Log("NPC has interacted");
            TalkButton.SetActive(false);

            StartCoroutine(DialogueManager.Instance.ShowDialogue(lines, hasName));
        }

    }

    public void showButton()
    {
        TalkButton.SetActive(true);
    }

    public void hideButton()
    {
        TalkButton.SetActive(false);
    }

    public void enableInteraction()
    {
        Debug.Log("npc is now able to interact");
        Interactable = true;
    }

    public void disableInteraction()
    {
        Interactable = false;
    }


    /*
    private void OnDialogueEnded()
    {
        if (hasAnimationToPlay && playAnimationOnDialogueEnd)
        {
            PlayAnimation();
            playAnimationOnDialogueEnd = false; // Reset flag
        }
    }

    
    private void PlayAnimation()
    {
        // Trigger animation here, if NPC has an Animator component
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("WalkOutAndDisappear");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to prevent memory leaks
        DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
    }
    */
}
