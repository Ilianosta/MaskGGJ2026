using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_JudgePanel : MonoBehaviour
{
    [SerializeField] SpriteRenderer suspectImg;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text[] options;
    [SerializeField] Button[] buttons;
    public void FillPanel(CrimeOptions crime, Sprite suspect)
    {
        ResetPanel();

        if (suspect != null) suspectImg.sprite = suspect;
        //questionText.text = crime.question;
        for (int i = 0; i < options.Length; i++)
        {
            options[i].text = crime.options[i].label;
            int actualIndex = i;
            buttons[i].onClick.AddListener(() =>
            {
                // Debug.Log("Clicking btn: " + actualIndex);
                UIManager.instance.onAnswerSelected?.Invoke(actualIndex);
            });
            buttons[i].GetComponent<ChoiceDetector>().index = actualIndex;
        }
    }

    public void ResetPanel()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].text = "";
            buttons[i].onClick.RemoveAllListeners();
        }
    }
}
