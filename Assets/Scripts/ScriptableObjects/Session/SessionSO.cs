using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SessionSO : ScriptableObject
{
    public int ID;
    public bool LoggedIn;

    public UnityEvent onLogin;
    public UnityEvent onLogout;

    public void Login(int id)
    {
        this.ID = id;
        LoggedIn = true;
        onLogin.Invoke();
    }

    public void Logout()
    {
        ID = -1;
        LoggedIn = false;
        onLogout.Invoke();
    }
}
