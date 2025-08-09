using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorVisuals : MonoBehaviour
{
    public CanvasGroup Canvas;
    public float start = 350f;
    public float end = 300f;

    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Character");
    }

    void Update()
    {
        if (Player == null) return;

        float Distance = Vector2.Distance(transform.position, Player.transform.position);
        Check(Distance);
    }

    void Check(float Dist)
    {
        Canvas.alpha = Mathf.InverseLerp(start, end, Dist);
        if (Dist > end) {
            Canvas.interactable = false;
        } else if (Dist < end) {
            Canvas.interactable = true;
        }
    }
}
