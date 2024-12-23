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
    private bool isPlaying;

    private void OnEnable()
    {
        currentFrame = 0;
        timer = 0;
        isPlaying = true;
        gifImage.sprite = frames[currentFrame];
    }

    public void Update()
    {
        if (!isPlaying || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame++;
            if (currentFrame >= frames.Length)
            {
                currentFrame = frames.Length - 1;
                isPlaying = false;
            }
            gifImage.sprite = frames[currentFrame];
        }
    }

    public bool IsAnimationFinished()
    {
        return !isPlaying;
    }
}
