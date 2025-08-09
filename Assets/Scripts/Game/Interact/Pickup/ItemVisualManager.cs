using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemVisuals : MonoBehaviour
{
    public TextMeshPro Txt;
    public CanvasGroup Canvas;
    public float start = 225f;
    public float end = 200f;

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
        Canvas.alpha = Mathf.Max(0.5f, Mathf.InverseLerp(start, end, Dist));
        if (Dist > end)
        {
            Canvas.interactable = false;
        }
        else if (Dist < end)
        {
            Canvas.interactable = true;
        }

        Txt.alpha = Mathf.InverseLerp(start, end, Dist);
    }
}
