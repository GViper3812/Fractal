using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Button Btn1;
    public Button Btn2;
    public Button Btn3;
    public Button Btn4;

    public Dictionary<int, Button> ButtonMap;

    void Awake()
    {
        ButtonMap = new Dictionary<int, Button>
        {
            { 1, Btn1 },
            { 2, Btn2 },
            { 3, Btn3 },
            { 4, Btn4 }
        };
    }

    public void SetButton(int Number, Action Function)
    {
        Button BTN = ButtonMap[Number];

        BTN.onClick.RemoveAllListeners();
        BTN.onClick.AddListener(() => Function());
    }
}
