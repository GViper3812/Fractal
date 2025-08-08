using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorLogic : MonoBehaviour
{
    public Movement Movement;
    public GameObject EnterMarker;
    public GameObject ExitMarker;

    public Toggle Toggle;

    private string DoorID;
    public DoorSO DoorSO;

    Vector2 EnterPos;
    Vector2 ExitPos;
    BoxCollider2D Door;

    Coroutine EnterCoroutine;
    Coroutine ExitCoroutine;

    GameObject Player;

    public string Scene;

    void Start()
    {
        Player = GameObject.Find("Character");
        Door = GetComponentInParent<BoxCollider2D>();

        EnterPos = new Vector2(EnterMarker.transform.position.x, EnterMarker.transform.position.y);
        ExitPos = new Vector2(ExitMarker.transform.position.x, ExitMarker.transform.position.y);

        DoorID = DoorSO.lastDoorID;
        Exit();

        Toggle.onValueChanged.AddListener(Enter);
    }

    public void Enter(bool isOn)
    {
        if (isOn)
        {
            DoorSO.lastDoorID = transform.parent.name;

            EnterCoroutine = StartCoroutine(ForceMovement(EnterPos, true));

            if (Door != null)
            {
                Door.enabled = false;
            }    
        } else if (EnterCoroutine != null) {
            StopCoroutine(EnterCoroutine);
        }
    }

    public void Exit()
    {
        if (!string.IsNullOrEmpty(DoorID))
        {
            Player.transform.position = EnterPos;

            ExitCoroutine = StartCoroutine(ForceMovement(ExitPos));

            if (Door != null)
            {
                Door.enabled = false;
            }
        }
        else if (ExitCoroutine != null)
        {
            StopCoroutine(ExitCoroutine);
        }
    }

    IEnumerator ForceMovement(Vector2 Dest, bool LoadScene = false)
    {
        while (Vector2.Distance(Player.transform.position, Dest) > 1)
        {
            Movement.ForcePos(Dest);

            yield return null;
        }

        if (Door != null)
        {
            Door.enabled = true;
        }

        if (Vector2.Distance(Player.transform.position, Dest) < 20 && LoadScene == true)
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
