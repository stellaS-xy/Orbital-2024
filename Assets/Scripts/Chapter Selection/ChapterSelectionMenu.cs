using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectionMenu : MonoBehaviour
{
    public Button[] chapterButtons; // Assign these in the inspector
    public string[] chapterNames; // Names of the chapters corresponding to buttons
    public Text[] chapterLabels; // Corresponding labels for the chapters

    private void Start()
    {
        UpdateButtonInteractability();
        DisplayLastVisitedLabel();
    }

    private void UpdateButtonInteractability()
    {
        for (int i = 0; i < chapterButtons.Length; i++)
        {
            string chapterName = chapterNames[i];
            if (i == 0 || ChapterManager.Instance.IsChapterStarted(chapterNames[i - 1]))
            {
                chapterButtons[i].interactable = true;
                int index = i; // Capture the current index in a local variable for the lambda
                chapterButtons[i].onClick.AddListener(() => StartChapter(chapterName));
            }
            else
            {
                chapterButtons[i].interactable = false;
            }
        }
    }

    private void DisplayLastVisitedLabel()
    {
        string lastVisitedChapter = ChapterManager.Instance.GetLastVisitedChapter();
        for (int i = 0; i < chapterNames.Length; i++)
        {
            if (chapterNames[i] == lastVisitedChapter)
            {
                chapterLabels[i].text = "Last Visited";
                chapterLabels[i].gameObject.SetActive(true);
            }
            else
            {
                chapterLabels[i].gameObject.SetActive(false);
            }
        }
    }

    private void StartChapter(string chapterName)
    {
        ChapterManager.Instance.SetChapterStarted(chapterName);
        ChapterManager.Instance.SetLastVisitedChapter(chapterName);
        SceneTransitionManager.Instance.TransitionToScene(chapterName); // Assuming scene names match chapter names
    }
}
