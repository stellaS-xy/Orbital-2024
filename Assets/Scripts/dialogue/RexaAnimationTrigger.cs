using System.Collections;
using UnityEngine;

public class RexaAnimationTrigger : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator PlayRexaAnimation()
    {
        Debug.Log("Playing Rexa animation...");
        animator.SetBool("WalkOutAndDisappear", true);

        // Wait for the animation to finish
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("RexaWalkOut") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        animator.SetBool("WalkOutAndDisappear", false);
        Debug.Log("Rexa animation finished.");
    }
}
