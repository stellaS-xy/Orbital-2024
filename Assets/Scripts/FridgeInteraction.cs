using UnityEngine;
using UnityEngine.Video;

public class FridgeInteraction : MonoBehaviour
{

    public GameObject videoPlayerObject;
    public VideoPlayer videoPlayer; 
    public KeyCode interactKey = KeyCode.E; 

    public bool isPlayerInRange = false;
    public bool isInteracting = false;

    private void Start()
    {
        if (videoPlayerObject == null || videoPlayer == null)
        {
            Debug.LogError("References to videoPlayerObject or videoPlayer are not assigned.");
            return;
        }

        videoPlayerObject.SetActive(false); // Ensure the video player is initially disabled
        videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the event when the video finishes playing
    }


    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey) && !isInteracting)
        {
            InteractWithFridge();
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
        Debug.Log("Bear interacted with the fridge and video starts to play");
        isInteracting = true;
        videoPlayerObject.SetActive(true); // Enable the video canvas
        Debug.Log("Playing video...");
        videoPlayer.Play(); // Play the video

        // Additional log to check if the video is prepared
        videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
        videoPlayer.Prepare();
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
        Debug.Log("Video is prepared and playing.");
        videoPlayer.Play();
    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("video done playing");
        videoPlayerObject.SetActive(false); // Disable the video canvas
        isInteracting = false;
    }
}
