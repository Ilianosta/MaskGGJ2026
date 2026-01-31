using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_JudgePanel : MonoBehaviour
{
    [SerializeField] SpriteRenderer suspectImg;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text[] options;
    public void FillPanel(CrimeOptions crime, Sprite suspect)
    {
        ResetPanel();

        if (suspect != null) suspectImg.sprite = suspect;
        //questionText.text = crime.question;
        for (int i = 0; i < options.Length; i++)
        {
            options[i].text = crime.options[i].label;

            Button optionsButton = options[i].GetComponentInParent<Button>();
            int actualIndex = i;

            optionsButton.onClick.AddListener(() =>
            {
                // Debug.Log("Clicking btn: " + actualIndex);
                UIManager.instance.onAnswerSelected?.Invoke(actualIndex);
            });
        }
    }

    public void ResetPanel()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].text = "";

            Button optionsButton = options[i].GetComponentInParent<Button>();
            int actualIndex = i;

            optionsButton.onClick.RemoveAllListeners();
        }
    }
}
