using UnityEngine;
using UnityEngine.UI;

public class ResetButtonHandler : MonoBehaviour
{
    public Button resetButton; // Assign this in the inspector

    private void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetPlayerPrefs);
        }
        else
        {
            Debug.LogError("Reset button is not assigned.");
        }
    }

    private void ResetPlayerPrefs()
    {
        if (ChapterManager.Instance != null)
        {
            ChapterManager.Instance.ResetPlayerPrefsData();
        }
        else
        {
            Debug.LogError("ChapterManager instance not found.");
        }
    }
}
