using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool isDone;
    public void Check()
    {
        if (!isDone)
        {
            isDone = true;
            // show the clue
            OnClickedAction();
        }
    }

    protected virtual void OnClickedAction()
    {

    }
}
