using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarFiller : MonoBehaviour
{
    [SerializeField] Image fillerImg;
    [SerializeField] Image fillerBackImg;
    [Tooltip("Optional")][SerializeField] TMP_Text percentageText;
    float currentAmount;

    [Header("Animation")]
    [SerializeField] float duration;
    [SerializeField] float differenceDuration;
    [SerializeField] LeanTweenType ease;

    void FillPercentage()
    {
        AnimateFilling(currentAmount);
        fillerImg.fillAmount = currentAmount;
        if (percentageText != null) percentageText.text = (currentAmount * 100).ToString() + "%";
    }

    public void SetCurrentValue(float amount)
    {
        currentAmount = Mathf.Clamp(amount, 0, 100);
        FillPercentage();
    }

    public void AddToCurrentAmount(float amount)
    {
        currentAmount = Mathf.Clamp(currentAmount + amount, 0, 1);
        FillPercentage();
    }

    void AnimateFilling(float amount)
    {
        LeanTween.value(fillerImg.fillAmount, amount, duration).setEase(ease).setOnUpdate((float v) =>
        {
            fillerImg.fillAmount = v;
        });
        LeanTween.value(fillerBackImg.fillAmount, amount, duration - differenceDuration).setEase(ease).setOnUpdate((float v) =>
        {
            fillerBackImg.fillAmount = v;
        });
    }

    [ContextMenu("Reset bar")]
    public void ResetBar()
    {
        SetCurrentValue(0);
    }
}
