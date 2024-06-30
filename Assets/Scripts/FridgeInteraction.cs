using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FridgeInteraction : MonoBehaviour
{
    public GameObject gifPlayerObject;
    public Image gifImage;
    public KeyCode interactKey = KeyCode.E;

    public bool isPlayerInRange = false;
    public bool isInteracting = false;
    private bool firstTimePlaying = true;
    public GameObject arrows;

    public string[] postVideoDialogue; // Dialogue lines to display after the video
    private DialogueManager dialogueManager;

    private gifPlayer gifPlayer;

    private void Start()
    {
        if (gifPlayerObject == null || gifImage == null)
        {
            Debug.LogError("References to gifPlayerObject or gifImage are not assigned.");
            return;
        }

        arrows.SetActive(false);
        gifPlayerObject.SetActive(false); // Ensure the GIF player is initially disabled

        dialogueManager = DialogueManager.Instance;
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager instance not found in the scene.");
        }

        gifPlayer = gifPlayerObject.GetComponent<gifPlayer>();
        if (gifPlayer == null)
        {
            Debug.LogError("gifPlayer component not found on gifPlayerObject.");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey) && !isInteracting && firstTimePlaying)
        {
            InteractWithFridge();
        }

        if (isInteracting && gifPlayer != null)
        {
            PlayGIF();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerInRange = true;
            Debug.Log("player is entered");
        }
        else
        {
            Debug.Log("is entered, but not player");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerInRange = false;
        }
    }

    private void InteractWithFridge()
    {
        Debug.Log("Bear interacted with the fridge and GIF starts to play");
        isInteracting = true;
        firstTimePlaying = false;
        gifPlayerObject.SetActive(true); // Enable the GIF player
    }

    private void PlayGIF()
    {
        // Check if the GIF has finished playing
        if (gifPlayer != null && gifPlayer.IsAnimationFinished())
        {
            OnGIFFinished();
        }
    }

    private void OnGIFFinished()
    {
        Debug.Log("GIF done playing");
        gifPlayerObject.SetActive(false); // Disable the GIF player
        isInteracting = false;
        arrows.SetActive(true);

        if (dialogueManager != null && postVideoDialogue != null && postVideoDialogue.Length > 0)
        {
            StartCoroutine(dialogueManager.ShowDialogue(postVideoDialogue, false));
        }
    }
}
