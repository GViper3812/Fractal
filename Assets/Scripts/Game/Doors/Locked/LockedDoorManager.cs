using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorManager : MonoBehaviour
{
    public GameObject Controls;
    public GameObject TextBox;
    public TextMeshPro Text;

    private string NewText = "Door cannot be unlocked";

    public Toggle Toggle;
    public Canvas Canvas;
    private bool Dismissable = false;

    public void ShowMessage() {
        if (Toggle.isOn == true) {
            StartCoroutine(Message());
        }
    }

    IEnumerator Message() {
        Controls.SetActive(false);
        TextBox.SetActive(true);
        Canvas.enabled = false;
        
        Text.text = NewText;

        Dismissable = false;

        yield return new WaitForSeconds(0.4f);

        Dismissable = true;

        while (!Input.GetMouseButtonDown(0)) {
            yield return null;
        }

        Text.text = "";

        Toggle.isOn = false;
        Canvas.enabled = true;
        TextBox.SetActive(false);
        Controls.SetActive(true);
    }
}
