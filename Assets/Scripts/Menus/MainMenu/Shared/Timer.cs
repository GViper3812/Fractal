using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float timer = 2f;
    private bool isTiming = false;

    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                stopTimer();
            }
        }
    }

    public void startTimer()
    {
        isTiming = true;
    }

    public void stopTimer()
    {
        isTiming = false;
        SceneManager.LoadScene("main");
    }
}
