using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScene : MonoBehaviour
{
    public Button skipButton;
    public string puzzleName;

    void Start()
    {
        if (PuzzleManager.Instance.IsPuzzleCompleted(puzzleName))
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);
        }
    }
}
