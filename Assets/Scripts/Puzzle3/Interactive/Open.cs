using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class open : MonoBehaviour
{
    public bool isDone = false;
     // open dialogue box upon click
    public void Click()
    {
        gameObject.SetActive(true);
        isDone = true;
    }

}
