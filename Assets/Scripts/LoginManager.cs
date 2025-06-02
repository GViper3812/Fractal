using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UsernameCheck : MonoBehaviour
{
    public string baseurl = "http://localhost:3000";

    public TMP_InputField usernameInput;
    public TMP_Text responseText;

    void Start()
    {
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseurl + "/login/Viper"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                Debug.Log(request.responseCode);
                Debug.Log(request.result);
            }
        }
    }
}



