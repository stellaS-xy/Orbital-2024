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
    public GameObject rabbit;
    public GameObject canvas;
    public GameObject grid;

    public Animator animator;

    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.eventObj);
        GameObject.DontDestroyOnLoad(this.gameController);
        GameObject.DontDestroyOnLoad(this.bear);
        GameObject.DontDestroyOnLoad(this.rabbit);
        GameObject.DontDestroyOnLoad(this.canvas);
        GameObject.DontDestroyOnLoad(this.grid);
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

    public IEnumerator LoadScene()
    {
        animator.SetBool("FadeIn", true);
        animator.SetBool("FadeOut", false);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.completed += OnLoadedScene;
    }

    private void OnLoadedScene(AsyncOperation obj)
    {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);

    }

   
    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("On triggered to load next scene");
                StartCoroutine(LoadScene());
            }
    }
}
