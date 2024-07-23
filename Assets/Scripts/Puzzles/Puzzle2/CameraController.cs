using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float speed;
    //public Transform target;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), speed*Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), speed*Time.deltaTime);
        }
        
    }


    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
}
*/
public class CameraController : MonoBehaviour
{
    public float speed = 10f;
    
    // Control camera movement using left/right arrow with boundary set to (0,20)
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Mathf.Min(speed * Time.deltaTime, transform.position.x));
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Mathf.Min(speed * Time.deltaTime, 20 - transform.position.x));
        }
    }
}