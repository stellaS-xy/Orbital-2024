using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    // close dialogue box upon click
    public void Click()
    {
        gameObject.SetActive(false);
    }
}
