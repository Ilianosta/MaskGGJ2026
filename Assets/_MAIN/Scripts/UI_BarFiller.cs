using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarFiller : MonoBehaviour
{
    [SerializeField] Image fillerImg;
    [Tooltip("Optional")][SerializeField] TMP_Text percentageText;
    float currentAmount;
    void FillPercentage(float amount)
    {
        currentAmount = amount;
        fillerImg.fillAmount = currentAmount;
        if (percentageText != null) percentageText.text = (currentAmount * 100).ToString() + "%";
    }

    public void SetCurrentValue(float amount)
    {
        currentAmount = Mathf.Clamp(amount, 0, 100);
        FillPercentage(currentAmount);
    }

    public void AddToCurrentAmount(float amount)
    {
        currentAmount = Mathf.Clamp(currentAmount + amount, 0, 1);
        FillPercentage(currentAmount);
    }
}
