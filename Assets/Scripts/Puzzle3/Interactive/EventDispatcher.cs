using UnityEngine;
using System.Collections;


public class EventDispatcher : MonoBehaviour
{

    public delegate void EventHandler(GameObject e);

    public event EventHandler MouseDown;
    public event EventHandler MouseDrag;
    public event EventHandler MouseUp;
    public event EventHandler MouseOver;
    public event EventHandler MouseExit;

    void OnMouseDown()
    {
        if (MouseDown != null)
        {
            MouseDown(this.gameObject);
        }
    }
    void OnMouseDrag()
    {
        if (MouseDrag != null)
        {
            MouseDrag(this.gameObject);  
        }           
    }
    void OnMouseUp()
    {
        if (MouseUp != null)
        {
            MouseUp(this.gameObject);
        }
    }
    void OnMouseOver()
    {
        if (MouseOver != null)
        {
            MouseOver(this.gameObject);
        }
    }
    void OnMouseExit()
    {
        if (MouseExit != null)
        {
            MouseExit(this.gameObject);
        }       
    }
}