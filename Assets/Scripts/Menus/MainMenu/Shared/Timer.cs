using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 2f;
    public bool isTiming = false;

    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isTiming = false;
                timer = 2f;
            }
        }
    }

    public void startTimer()
    {
        isTiming = true;
    }
}