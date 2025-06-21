using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontManager : MonoBehaviour
{
    [SerializeField] public SessionSO session;
    public Timer Timer;

    public MainManager MainMan;

    public CameraManager Camera;

    public GameObject LoginUI;
    public GameObject MainUI;

    public GameObject FrontUI;
    public GameObject BeginUI;

    public void Begin()
    {
        FrontUI.SetActive(false);
        BeginUI.SetActive(true);

        Timer.startTimer();

        StartCoroutine(TimerStop());
    }

    public void Continue()
    {

    }

    public void Options()
    {

    }

    public void Logout()
    {
        session.Logout();

        Camera.MoveTo(MainUI.transform.position, LoginUI.transform.position);
    }

    void Start()
    {
        MainMan.SetButton(1, Begin);
        /* MainMan.SetButton(2, Continue);
        MainMan.SetButton(3, Options); */
        MainMan.SetButton(5, Logout);
    }

    private IEnumerator TimerStop()
    {
        yield return new WaitWhile(() => Timer.isTiming);

        SceneManager.LoadScene("LVL1 Entrance");
    }
}
