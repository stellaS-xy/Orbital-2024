using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoxAnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayed;
    public NPCController fox;
    public GameObject foxObject;
    public NPCController rabbit;

    public GameObject bookShelfObject;


    private void Start()
    {
        animator = GetComponent<Animator>();
        DialogueManager.Instance.OnDialogueEnded += PlayFoxAnimation;
        hasPlayed = false;
        rabbit.disableInteraction();

    }

    private void PlayFoxAnimation()
    {
        if (!hasPlayed && animator != null)
        {
            animator.SetBool("ShouldWalkOut", true); // Ensure this is set to true
            hasPlayed = true;
            fox.disableInteraction();

            StartCoroutine(WaitForAnimationToEnd());
        }
    }

    private IEnumerator WaitForAnimationToEnd()
    {

        Debug.Log("WaitForAnimationToEnd has been called");
        // Wait until the animation state is playing

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("foxOutofHouse"))
        {
            Debug.Log("Waiting for foxOutofHouse state to start...");
            yield return null;
        }

        Debug.Log("foxOutofHouse state has started.");

        // Wait until the animation is done
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("foxOutofHouse"))
        {
            Debug.Log("foxOutofHouse state is playing...");
            yield return null;
        }

        Debug.Log("foxOutofHouse state has ended.");

        
        animator.SetBool("ShouldWalkOut", false); // Reset the boolean to prevent re-triggering

        foxObject.SetActive(false);
        fox.hideButton();

        rabbit.showButton();
        rabbit.enableInteraction();
        Debug.Log("Rabbit is now ready to interact");

        bookShelfObject.SetActive(true);


    }

    private void Update()
    {
        // Check if the animation has started playing and set the bool back to false
        if (hasPlayed && animator.GetCurrentAnimatorStateInfo(0).IsName("foxOutofHouse"))
        {
            animator.SetBool("ShouldWalkOut", false); // Reset the boolean to prevent re-triggering
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to prevent memory leaks
        DialogueManager.Instance.OnDialogueEnded -= PlayFoxAnimation;
    }
}
