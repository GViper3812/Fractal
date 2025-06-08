using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform LoginUI;
    public RectTransform RegisterUI;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(LoginUI);

        float widthA = LoginUI.rect.width;

        RegisterUI.anchoredPosition = new Vector2(widthA, RegisterUI.anchoredPosition.y);
    }
}
