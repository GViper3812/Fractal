using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform LoginUI;
    public RectTransform RegisterCheckUI;
    public RectTransform RegisterConfirmUI;
    public RectTransform MainMenuUI;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(LoginUI);

        float height = LoginUI.rect.height;
        float width = LoginUI.rect.width;

        MainMenuUI.anchoredPosition = new Vector2(MainMenuUI.anchoredPosition.x, height * 2);

        RegisterCheckUI.anchoredPosition = new Vector2(width, RegisterCheckUI.anchoredPosition.y);
        RegisterConfirmUI.anchoredPosition = new Vector2(width, RegisterConfirmUI.anchoredPosition.y);
    }
}
