using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance { get; private set; }

    private const string LastVisitedChapterKey = "LastVisitedChapter";
    private const string StartedChaptersKey = "StartedChapters";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetChapterStarted(string chapterName)
    {
        PlayerPrefs.SetInt(chapterName, 1);
        PlayerPrefs.Save();
    }

    public bool IsChapterStarted(string chapterName)
    {
        return PlayerPrefs.GetInt(chapterName, 0) == 1;
    }

    public void SetLastVisitedChapter(string chapterName)
    {
        PlayerPrefs.SetString(LastVisitedChapterKey, chapterName);
        PlayerPrefs.Save();
    }

    public string GetLastVisitedChapter()
    {
        return PlayerPrefs.GetString(LastVisitedChapterKey, string.Empty);
    }
}
