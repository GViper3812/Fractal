using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    GameObject Player;

    BoxCollider2D Door;

    float Distance;
    public float start = 350f;
    public float end = 300f;

    public CanvasGroup Canvas;

    public Movement Movement;

    public GameObject Marker;
    Vector2 MarkPos;
    Vector2 PlayPos;

    public Toggle Toggle;

    void Start()
    {
        Toggle.onValueChanged.AddListener(Enter);

        MarkPos = new Vector2(Marker.transform.position.x, Marker.transform.position.y);

        Player = GameObject.Find("Character");

        Door = GetComponentInParent<BoxCollider2D>();
    }

    void Update()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);

        Check(Distance);

        PlayPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
    }

    void Check(float dist)
    {
        Canvas.alpha = Mathf.InverseLerp(start, end, dist);

        if (dist > end)
        {
            Canvas.interactable = false;
        }
        else if (dist < end)
        {
            Canvas.interactable = true;
        }
    }

    Coroutine EnterCoroutine;

    public void Enter(bool isOn)
    {
        if (isOn)
        {
            EnterCoroutine = StartCoroutine(ForceMovement());

            Door.enabled = false;
        }
        else if (EnterCoroutine != null)
        {
            StopCoroutine(EnterCoroutine);
        }
    }

    IEnumerator ForceMovement()
    {
        while (true)
        {
            Movement.ForcePos(MarkPos);
            yield return null;
        }
    }
}
