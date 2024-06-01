using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public CardState cardState;

    
    void Start()
    {
        cardState = CardState.unflipped;
    }

    private void OnMouseUp()
    {
        OpenCard();
    }
    
    void OpenCard()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        cardState = CardState.flipped;
    }
    
    public enum CardState 
    {
        unflipped, flipped, matched
    }
}
