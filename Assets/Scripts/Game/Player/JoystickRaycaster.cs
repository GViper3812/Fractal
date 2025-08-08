using UnityEngine;

public class JoystickRaycaster2D : MonoBehaviour
{
    GameObject Player;

    public FixedJoystick directionJoystick;
    public float rayDistance = 10000f;
    public LayerMask collisionLayers;

    public LineRenderer line;

    void Start()
    {
        Player = GameObject.Find("Character");
    }

    void Update()
    {
        Vector2 dir = new Vector2(directionJoystick.Horizontal, directionJoystick.Vertical);

        if (dir.magnitude > 0.1f)
        {
            dir.Normalize();
            Vector2 origin = new Vector2(Player.transform.position.x, Player.transform.position.y);
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
            line.enabled = false;
        }
    }
}
