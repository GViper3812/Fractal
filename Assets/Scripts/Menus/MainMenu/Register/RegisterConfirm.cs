using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class RegisterConfirm: MonoBehaviour
{
    public string uri = "http://localhost:3000/register/confirm";

    public RegisterCheck Check;

    public TMP_InputField passwordInput;
    public TMP_InputField confirmInput;
    public TMP_Text responseText;

    public Button registerButton;
    public Button backButton;

    [SerializeField] private Timer Timer;

    public CameraManager Camera;
    public GameObject RegisterCheckUI;
    public GameObject RegisterConfirmUI;
    public GameObject MainUI;

    void Start()
    {
        registerButton.onClick.AddListener(registerRequest);
        backButton.onClick.AddListener(Back);
    }

    void Back()
    {
        RegisterConfirmUI.SetActive(false);
        RegisterCheckUI.SetActive(true);
    }

    void registerRequest() => StartCoroutine(Register_Coroutine());

    IEnumerator Register_Coroutine()
    {
        if (passwordInput.text != "" && confirmInput.text != "" && passwordInput.text == confirmInput.text)
        {
            RegisterRequest confirm = new RegisterRequest();
            confirm.username = Check.next.username.ToLower();
            confirm.email = Check.next.email.ToLower();
            confirm.password = passwordInput.text;

            string json = JsonUtility.ToJson(confirm);
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
                    RegisterResponse response = JsonUtility.FromJson<RegisterResponse>(responseJson);

                    if (response.success)
                    {
                        Timer.startTimer();
                        responseText.color = new Color32(0, 160, 0, 255);
                        responseText.text = response.message;

                        Vector3 regPos = RegisterConfirmUI.transform.position;
                        Vector3 mainPos = MainUI.transform.position;

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
                    NextResponse response = JsonUtility.FromJson<NextResponse>(responseJson);
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
        Vector3 regPos = RegisterConfirmUI.transform.position;
        Vector3 mainPos = MainUI.transform.position;

        yield return new WaitWhile(() => Timer.isTiming);

        Camera.MoveTo(regPos, mainPos);
    }
}

[System.Serializable]
public class RegisterRequest
{
    public string username;
    public string email;
    public string password;
}

[System.Serializable]
public class RegisterResponse
{
    public bool success;
    public string errorType;
    public string message;
}
