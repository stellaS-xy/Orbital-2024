using UnityEngine;

public class ChapterCompletionHandler : MonoBehaviour
{
    public string currentChapterName;

    public void CompleteChapter()
    {
        // Mark the current chapter as completed
        ChapterManager.Instance.SetChapterStarted(currentChapterName);

        // Get the next chapter
        string nextChapter = ChapterManager.Instance.GetNextChapter(currentChapterName);

        if (nextChapter != null)
        {
            // Mark the next chapter as started
            ChapterManager.Instance.SetChapterStarted(nextChapter);

            // Set the last visited chapter
            ChapterManager.Instance.SetLastVisitedChapter(nextChapter);

            // Transition to the next chapter
            //SceneTransitionManager.Instance.TransitionToScene(nextChapter);
        }
        else
        {
            Debug.Log("All chapters completed!");
        }
    }
}
