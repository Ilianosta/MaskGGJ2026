using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject interrogatory;
    [SerializeField] UI_JudgePanel judgePanel;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueTxt;
    [SerializeField] UI_BarFiller suspectBar;
    [SerializeField] Q_Vignette_Single vignette;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject finalCosa;
    public delegate void OnAnswerSelected(int index);
    public OnAnswerSelected onAnswerSelected;
    public bool onFinish = false;
    void Awake()
    {
        if (UIManager.instance != null) Destroy(gameObject);
        else UIManager.instance = this;

        DontDestroyOnLoad(gameObject);

        suspectBar.SetCurrentValue(0);
    }

    void Start()
    {
        DialogueManager.onStartDialogue += () =>
        {
            if (GameManager.instance.gameStarted) ShowInterrogatory(false);
        };

        DialogueManager.onFinishDialogue += () =>
        {
            if (GameManager.instance.gameStarted && !onFinish) ShowInterrogatory(true);
        };
        ShowInterrogatory(false);
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

    public void ShowInterrogatory(bool show)
    {
        interrogatory.SetActive(show);
    }

    public void ShowBlackScreen(bool show)
    {
        if (show)
        {
            blackScreen.CrossFadeAlpha(255, .25f, true);
        }
        else
        {
            blackScreen.CrossFadeAlpha(0, .25f, true);
        }
    }

    public void ShowEndScreen()
    {
        finalCosa.SetActive(true);
    }
}