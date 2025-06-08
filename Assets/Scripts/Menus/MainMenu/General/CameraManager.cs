using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public float moveTime = 0.5f;
    private Coroutine moveCoroutine;

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
}