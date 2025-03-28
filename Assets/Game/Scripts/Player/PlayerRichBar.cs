using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRichBar : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI title;

    public void SetRichBar(float value, string titleText, Color color)
    {
        fillImage.fillAmount = value;
        fillImage.color = color;
        title.text = titleText;
    }
}