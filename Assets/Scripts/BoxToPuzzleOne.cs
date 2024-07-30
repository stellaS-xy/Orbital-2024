using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxToPuzzleOne : MonoBehaviour
{

    [SerializeField] private bool isEntered;
    public GameObject eventObj;
    public GameObject gameController;
    public GameObject bear;
 
    public GameObject canvas;
    public GameObject grid;
    public GameObject screenImage;

    public Animator animator;
    public ChapterCompletionHandler chapterCompletionHandler;
    public string nextSceneName;

    private void Start()
    {
        //GameObject.DontDestroyOnLoad(this.canvas);
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("player"))
        {
            isEntered = true;
            Debug.Log("player is entered");
            
            
        }
        else
        {
            Debug.Log("is entered, but not player");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isEntered = false;
        }
    }

    private IEnumerator LoadScene()
    {
        Debug.Log("faded out");
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);

        yield return new WaitForSeconds(2);

        // Use SceneTransitionManager to load the next scene
        SceneTransitionManager.Instance.TransitionToScene(nextSceneName);
        
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("On triggered to load next scene");
            StartCoroutine(LoadScene());
            if (chapterCompletionHandler != null)
            {
                chapterCompletionHandler.CompleteChapter();
            }
            else
            {
                Debug.LogWarning("ChapterCompletionHandler is not assigned. Skipping chapter completion.");
            }
        }
    }
}
