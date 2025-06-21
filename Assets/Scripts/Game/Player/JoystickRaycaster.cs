using UnityEngine;

public class JoystickRaycaster2D : MonoBehaviour
{
    public FixedJoystick directionJoystick;
    public float rayDistance = 10000f;
    public LayerMask collisionLayers;

    public LineRenderer line;

    void Update()
    {
        Vector2 dir = new Vector2(directionJoystick.Horizontal, directionJoystick.Vertical);

        if (dir.magnitude > 0.1f)
        {
            dir.Normalize();
            Vector2 origin = new Vector2(transform.position.x, transform.position.y - 100);
            Vector2 endPoint = origin + dir * rayDistance;

            line.enabled = true;
            line.SetPosition(0, origin);
            line.SetPosition(1, endPoint);

            RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, collisionLayers);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
        else
        {
            // Hide line when joystick is idle
            line.enabled = false;
        }
    }
}
