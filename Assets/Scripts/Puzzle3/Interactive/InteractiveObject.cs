using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveObject : MonoBehaviour, IPointerClickHandler
{
    public GameObject canvasToShow;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Object clicked: " + gameObject.name);
        ShowCanvas();
        
        ClueManager.Instance.CollectClue(gameObject.name);
    }

    private void ShowCanvas()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas to show is not assigned.");
        }
    }
}
