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
    public GameObject screenImage;

    public Animator animator;

    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.canvas);
       
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
        
        Debug.Log("faded out");
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);
        


        yield return new WaitForSeconds(2);
        

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.completed += OnLoadedScene;
    }

    private void OnLoadedScene(AsyncOperation obj)
    {
        
        Debug.Log("Animator: FadeIn set to true");

        animator.SetBool("FadeIn", true);
        animator.SetBool("FadeOut", false);


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
