using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public bool isMonitor;
    public chapter5SceneController sceneController;

    private void OnMouseDown()
    {
        if (isMonitor)
        {
            Debug.Log("Monitor clicked");
            sceneController.InteractWithMonitor();
        }
        else
        {
            Debug.Log("File clicked");
            sceneController.InteractWithFiles();
        }
    }
}
