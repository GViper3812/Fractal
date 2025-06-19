using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D RB;
    public float Acceleration = 500f;
    public float MaxSpeed = 150f;

    public FixedJoystick joystick;

    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(-joystick.Horizontal, -joystick.Vertical);

        if (inputDir.magnitude > 0.1f)
        {
            RB.drag = 0;
            RB.AddForce(inputDir.normalized * Acceleration);

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
}
