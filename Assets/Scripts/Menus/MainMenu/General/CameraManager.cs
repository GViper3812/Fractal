using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public float moveTime = 0.5f;
    private Coroutine moveCoroutine;
    public GameObject MainUI;

    public Camera Camera;

    public SessionSO session;

    public void MoveTo(Vector3 fromPos, Vector3 toPos)
    {
        fromPos.z -= 10f;
        toPos.z -= 10f;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(SmoothMove(fromPos, toPos));
    }

    private IEnumerator SmoothMove(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            float t = elapsed / moveTime;

            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }

    void Start()
    {
        if (session.LoggedIn == true)
        {
            Vector3 TopPos = new Vector3(MainUI.transform.position.x, MainUI.transform.position.y + (3f * Camera.orthographicSize), MainUI.transform.position.z);
            

            MoveTo(TopPos, MainUI.transform.position);
        }
    }
}