using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginManager : MonoBehaviour
{
    public GameObject BeginUI;

    void Start()
    {
        BeginUI.SetActive(false);
    }

}
