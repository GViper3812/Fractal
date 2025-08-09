using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorVisuals : MonoBehaviour
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

    public Image Image;
    public float FadeTime = 0.6f;

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color Col = Image.color;

        while (elapsed < FadeTime)
        {
            Col.a = Mathf.Lerp(0, 1, elapsed / FadeTime);
            Image.color = Col;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color Col = Image.color;

        while (elapsed < FadeTime)
        {
            Col.a = Mathf.Lerp(1, 0, elapsed / FadeTime);
            Image.color = Col;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
