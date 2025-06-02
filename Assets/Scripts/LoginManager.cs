using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public string baseurl = "http://localhost:3000";

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text responseText;

    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        loginButton.onClick.AddListener(GetData);
        registerButton.onClick.AddListener(LoadRegister);
    }

    void LoadRegister()
    {
        SceneManager.LoadScene("register");
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        if (usernameInput.text != "" && passwordInput.text != "")
        {
            using (UnityWebRequest request = UnityWebRequest.Get(baseurl + "/login/" + usernameInput.text.ToLower() + "/" + passwordInput.text))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                    Debug.LogError(request.error);
                else if (request.downloadHandler.text == "Valid")
                {
                    SceneManager.LoadScene("main");
                }
                else
                {
                    responseText.text = request.downloadHandler.text;
                }
            }
        } else {
            responseText.text = "Please fill all fields";
        }
    }
}
