using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class RegisterCheck : MonoBehaviour
{
    public string uri = "http://localhost:3000/register/check";

    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_Text responseText;

    public Button nextButton;
    public Button backButton;

    [SerializeField] private Timer Timer;

    public CameraManager Camera;
    public GameObject RegisterCheckUI;
    public GameObject RegisterConfirmUI;
    public GameObject LoginUI;

    [HideInInspector] public NextRequest next;

    void Start()
    {
        nextButton.onClick.AddListener(NextRequest);
        backButton.onClick.AddListener(Back);
        RegisterConfirmUI.SetActive(false);
    }

    void Back()
    {
        Vector3 regPos = RegisterCheckUI.transform.position;
        Vector3 logPos = LoginUI.transform.position;

        Camera.MoveTo(regPos, logPos);
    }

    void NextRequest() => StartCoroutine(Next_Coroutine());

    IEnumerator Next_Coroutine()
    {
        if (usernameInput.text != "" && emailInput.text != "")
        {
            next = new NextRequest();
            next.username = usernameInput.text.ToLower();
            next.email = emailInput.text.ToLower();

            string json = JsonUtility.ToJson(next);
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
                    NextResponse response = JsonUtility.FromJson<NextResponse>(responseJson);

                    if (response.success)
                    {
                        Timer.startTimer();
                        responseText.color = new Color32(0, 160, 0, 255);
                        responseText.text = response.message;

                        RegisterCheckUI.SetActive(false);
                        RegisterConfirmUI.SetActive(true);
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
}

[System.Serializable]
public class NextRequest
{
    public string username;
    public string email;
}

[System.Serializable]
public class NextResponse
{
    public bool success;
    public string errorType;
    public string message;
}
