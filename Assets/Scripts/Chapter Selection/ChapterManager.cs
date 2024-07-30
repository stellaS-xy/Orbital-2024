using UnityEngine;
using System;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance { get; private set; }

    public string[] chapterNames; // Assign these in the inspector

    private const string LastVisitedChapterKey = "LastVisitedChapter";

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

    public void ResetPlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public string GetNextChapter(string currentChapter)
    {
        int currentIndex = Array.IndexOf(chapterNames, currentChapter);
        if (currentIndex >= 0 && currentIndex < chapterNames.Length - 1)
        {
            return chapterNames[currentIndex + 1];
        }
        return null; // No more chapters
    }
}
