using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Camera mainCam;
    public Canvas Canvas;

    void Start()
    {
        float height = 1.677f * mainCam.orthographicSize;
        float width = height * mainCam.aspect;

        RectTransform rt = Canvas.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
    }
}