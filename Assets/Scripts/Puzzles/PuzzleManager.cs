using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Dictionary to store the completion status of puzzles
    private Dictionary<string, bool> puzzleCompletionStatus = new Dictionary<string, bool>();

    // Singleton instance
    public static PuzzleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the manager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to set the completion status of a puzzle
    public void SetPuzzleCompleted(string puzzleName)
    {
        if (puzzleCompletionStatus.ContainsKey(puzzleName))
        {
            puzzleCompletionStatus[puzzleName] = true;
        }
        else
        {
            puzzleCompletionStatus.Add(puzzleName, true);
        }
    }

    // Method to check if a puzzle is completed
    public bool IsPuzzleCompleted(string puzzleName)
    {
        if (puzzleCompletionStatus.ContainsKey(puzzleName))
        {
            return puzzleCompletionStatus[puzzleName];
        }
        return false;
    }
}
