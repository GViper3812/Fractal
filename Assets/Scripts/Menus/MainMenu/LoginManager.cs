using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public string uri = "http://localhost:3000/login";
    [SerializeField] private SessionSO session;

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text responseText;

    public Button loginButton;
    public Button registerButton;

    [SerializeField] private Timer Timer;

    public CameraManager Camera;
    public GameObject LoginUI;
    public GameObject RegisterUI;
    public GameObject MainUI;

    void Start()
    {
        loginButton.onClick.AddListener(LoginRequest);
        registerButton.onClick.AddListener(LoadRegister);
    }

    void LoadRegister()
    {
        Vector3 logPos = LoginUI.transform.position;
        Vector3 regPos = RegisterUI.transform.position;

        Camera.MoveTo(logPos, regPos);
    }

    void LoginRequest() => StartCoroutine(Login_Coroutine());

    IEnumerator Login_Coroutine()
    {
        if (usernameInput.text != "" && passwordInput.text != "")
        {
            LoginRequest login = new LoginRequest();
            login.username = usernameInput.text.ToLower();
            login.password = passwordInput.text;

            string json = JsonUtility.ToJson(login);
            byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

            using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(body);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string responseJson = request.downloadHandler.text;
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseJson);

                    if (response.success == true)
                    {
                        session.Login(response.id);

                        Timer.startTimer();
                        responseText.color = new Color32(0, 160, 0, 255);
                        responseText.text = response.message;

                        StartCoroutine(TimerStop());
                    }
                    else
                    {
                        responseText.text = response.message;
                    }
                }
                else if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    string responseJson = request.downloadHandler.text;
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseJson);
                    responseText.text = response.message;
                }
                else
                {
                    responseText.text = "Connection Error";
                }
            }
        }
        else
        {
            responseText.text = "Please fill all fields";
        }
    }

    private IEnumerator TimerStop()
    {
        Vector3 logPos = LoginUI.transform.position;
        Vector3 mainPos = MainUI.transform.position;

        yield return new WaitWhile(() => Timer.isTiming);

        Camera.MoveTo(logPos, mainPos);

        responseText.text = string.Empty;
        usernameInput.text = string.Empty;
        passwordInput.text = string.Empty;

        responseText.color = new Color32(130, 0, 0, 255);
    }
}

[System.Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string errorType;
    public string message;
    public int id;
}
