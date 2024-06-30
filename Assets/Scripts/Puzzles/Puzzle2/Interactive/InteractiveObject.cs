using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject canvasToShow;
    public bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowCanvas();
            ClueManager.Instance.CollectClue(gameObject.name);
        }
    }

    private void ShowCanvas()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(true);
            Debug.Log("Canvas shown: " + canvasToShow.name);
        }
        else
        {
            Debug.LogError("Canvas to show is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered interaction area: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited interaction area: " + gameObject.name);
        }
    }
}
