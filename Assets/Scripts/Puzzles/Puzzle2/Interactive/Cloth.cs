using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour {

    // Use this for initialization
    void Start() {

        GameObject clickObj = GameObject.Find("cloth");
        Debug.Log(clickObj);
        if (clickObj != null) {
            clickObj.GetComponent<EventDispatcher>().MouseDown += OnMouseDown;          
        }

      
 }
    private void OnMouseDown(GameObject e)
    {
        Debug.Log("点击物体："+e);
    }

}
