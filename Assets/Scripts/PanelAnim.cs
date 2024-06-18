using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;
    public GameObject panel;

    [SerializeField] private bool panelClosed = true;

    IEnumerator ShowPanel(GameObject gameObject) 
    {
        float timer = 0;
        while (timer <= 1)
        {
           gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer); 
           timer += Time.deltaTime * animationSpeed;
           yield return null; //wait for one frame
        } 
    }

    IEnumerator HidePanel(GameObject gameObject) 
    {
        float timer = 0;
        while (timer <= 1)
        {
           gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer); 
           timer += Time.deltaTime * animationSpeed;
           yield return null; //wait for one frame
        }
    }

    private void Update()
    {
        if (panelClosed) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(ShowPanel(panel));
                panelClosed = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(HidePanel(panel));
                panelClosed = true;
            }
/*
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShowPanel(panel));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(HidePanel(panel));
        }
*/
    }
}
