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
    [SerializeField] UI_BarFiller suspectTimerBar;
    public Timer suspectTimer;
    public delegate void OnAnswerSelected(int index);
    public OnAnswerSelected onAnswerSelected;
    void Awake()
    {
        if (UIManager.instance != null) Destroy(gameObject);
        else UIManager.instance = this;

        DontDestroyOnLoad(gameObject);

        suspectBar.SetCurrentValue(0);
    }

    void Update()
    {
        if (!suspectTimer.IsWorking) return;
        suspectTimerBar.SetCurrentValue(suspectTimer.CurrentTime / suspectTimer.MaxTime);
    }

    public void ShowJudgePanel(CrimeOptions crime, Sprite suspect) => judgePanel.FillPanel(crime, suspect);

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
}