using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   public float moveSpeed;

   public bool isMoving;

   public Vector2 input; 

   public LayerMask solidObjectLayer;
   public LayerMask interactablesLayer;

   private Animator animator;

   private void Awake()
   {
        animator = GetComponent<Animator>();
   }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWalkable(targetPos)) 
                {
                    StartCoroutine(Move(targetPos));

                }
                
            }
        }

        animator.SetBool("isMoving",isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        transform.position = targetPos;

    }

    private bool isWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer | interactablesLayer) != null)
        {
            return false;
        }
        return true;
    }
}

