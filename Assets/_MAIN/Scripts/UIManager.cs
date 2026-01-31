using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] UI_JudgePanel judgePanel;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueTxt;
    [SerializeField] UI_BarFiller suspectBar;
    [SerializeField] Q_Vignette_Single vignette;
    public delegate void OnAnswerSelected(int index);
    public OnAnswerSelected onAnswerSelected;

    void Awake()
    {
        if (UIManager.instance != null) Destroy(gameObject);
        else UIManager.instance = this;

        DontDestroyOnLoad(gameObject);

        suspectBar.SetCurrentValue(0);

        DialogueManager.onStartDialogue += () => CloseJudgePanel();
        DialogueManager.onFinishDialogue += () => OpenJudgePanel();
    }

    public void ShowJudgePanel(CrimeOptions crime, Sprite suspect) => judgePanel.FillPanel(crime, suspect);

    public void AnimateVignette()
    {
        LeanTween.value(0f, 2.5f, 0.2f).setEaseOutQuint().setLoopPingPong(1).setOnUpdate((float v) =>
        {
            vignette.mainScale = v;
        });
    }

    public void SetDialogueText(string txt)
    {
        dialogueTxt.text = txt;
    }

    public void FillSuspectPercentage(float amount)
    {
        suspectBar.AddToCurrentAmount(amount / 100);
    }

    public void OpenDialoguePanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void OpenJudgePanel()
    {
        judgePanel.gameObject.SetActive(true);
    }

    public void CloseJudgePanel()
    {
        judgePanel.gameObject.SetActive(false);
    }
}