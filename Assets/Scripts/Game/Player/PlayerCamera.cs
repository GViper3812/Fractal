using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject character;
    public Camera cam;


    void Update()
    {
        Vector3 charPos = new Vector3(character.transform.position.x, character.transform.position.y + 150, -1000);

        cam.transform.position = charPos;
    }
}
