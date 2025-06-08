using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Camera mainCam;

    void Start()
    {
        float height = 1.673f * mainCam.orthographicSize;
        float width = height * mainCam.aspect;

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
        transform.position = mainCam.transform.position + mainCam.transform.forward * 1f;
    }
}