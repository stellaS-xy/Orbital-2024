using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gifPlayer : MonoBehaviour
{
    public Image gifImage; // The UI Image component to display the frames
    public Sprite[] frames; // Array to hold the frames
    public float frameRate = 10f; // Frames per second

    private int currentFrame;
    private float timer;

    public void Update()
    {
        Debug.Log("GIFPlayer update is called");
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;
            gifImage.sprite = frames[currentFrame];
        }
    }

    public bool IsAnimationFinished()
    {
        return currentFrame == frames.Length - 1;
    }
}
