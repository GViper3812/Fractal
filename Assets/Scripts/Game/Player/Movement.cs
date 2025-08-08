using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D RB;
    public float Acceleration = 500f;
    public float MaxSpeed = 150f;

    public FixedJoystick joystick;

    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        ForceDir(inputDir);
    }

    void ForceDir(Vector2 Dir)
    {
        if (Dir.magnitude > 0.1f)
        {
            RB.drag = 0;
            RB.AddForce(Dir * Acceleration);

            if (RB.velocity.magnitude > MaxSpeed)
            {
                RB.velocity = RB.velocity.normalized * MaxSpeed;
            }
        }
        else
        {
            RB.drag = 15;
        }
    }

    public void ForcePos(Vector2 Pos)
    {
        Vector2 Dir = (Pos - RB.position).normalized;

        if (Dir.magnitude > 0.1f)
        {
            RB.drag = 0;
            RB.AddForce(Dir * Acceleration);

            if (RB.velocity.magnitude > 1000)
            {
                RB.velocity = RB.velocity.normalized * 1000;
            }
        }
        else
        {
            RB.drag = 15;
        }
    }
}
