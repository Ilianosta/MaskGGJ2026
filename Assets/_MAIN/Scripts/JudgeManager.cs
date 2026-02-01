using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class JudgeManager : MonoBehaviour
{
    public LevelData[] crimes;
    [SerializeField] private int currentCrimeIndex = 0;
    [SerializeField] private int currentCrimeQuestion = 0;
    int currentSuspectState = 0;

    private AudioManager audioManager;

    [Header("Animations")]
    [SerializeField] GameObject imgSuspect;
    [SerializeField] float durationX, durationY;
    [SerializeField] LeanTweenType ease;
    [SerializeField] AnimationCurve curveAnimWrongAnswer;
    [SerializeField] Vector2 scale;
    [SerializeField] CameraShake2D cameraShake2D;
    [SerializeField] UIShake uIShake;

    // HELPERS
    LevelData CurrentCrime => crimes[currentCrimeIndex];
    CrimeOptions CurrentCrimeOptions => CurrentCrime.crimeOptions[currentCrimeQuestion];
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        UIManager.instance.onAnswerSelected += ReceiveAnswer;
    }

    public void SetCurrentCrime(int index) => currentCrimeIndex = index;

    [ContextMenu("CreateCrimeOption")]
    public void CreateCrimeOptions()
    {
        LevelData crime = crimes[currentCrimeIndex];
        UIManager.instance.ShowJudgePanel(crime.crimeOptions[currentCrimeQuestion], crime.suspectImg[currentSuspectState]);
    }

    public void ReceiveAnswer(int index)
    {
        audioManager.PlaySFX(audioManager.choiceSFX);
        Option option = CurrentCrimeOptions.options[index];
        Debug.Log("Option correct percentage: " + option.correctPercentage);
        if (option.correctPercentage > 0) OnReceiveCorrectAnswer();
        else OnReceiveWrongAnswer();

        if (option.answer.Length > 0) DialogueManager.instance.StartDialogue(option.answer);
        UIManager.instance.FillSuspectPercentage(option.correctPercentage);
    }

    void GoNextQuestion()
    {
        currentSuspectState++;
        currentCrimeQuestion++;
        if (2 == currentSuspectState || 3 == currentSuspectState)
            audioManager.PlaySFX(audioManager.breakSFX);
        if (4 == currentSuspectState)
            audioManager.PlaySFX(audioManager.fullBreakSFX);

        if (currentCrimeQuestion > CurrentCrime.crimeOptions.Length - 1)
        {
            GameManager.instance.EndGame();
            Debug.Log("END GAME");
        }
        else
        {
            CreateCrimeOptions();
            Debug.Log("CONTINUE");
        }
    }

    void OnReceiveCorrectAnswer()
    {
        cameraShake2D.Shake();
        uIShake.Shake();
        LeanTween.cancel(gameObject);
        LeanTween.scaleX(imgSuspect, scale.x, durationX).setEase(ease).setLoopPingPong(1);
        LeanTween.scaleY(imgSuspect, scale.y, durationY).setEase(ease).setLoopPingPong(1);
        UIManager.instance.AnimateVignette();
        if (currentCrimeQuestion == 3)
        {
            Debug.Log("Final Alcansado");
            StartCoroutine(suspensiveEnd());
        }
        else
        {
            GoNextQuestion();
            Debug.Log("CORRECTO");
        }
            
    }
    [ContextMenu("AnimWrongAnswer")]
    void OnReceiveWrongAnswer()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(imgSuspect, scale * 1.15f, 2).setEase(curveAnimWrongAnswer);
        Debug.Log("INCORRECTO");
    }
    
    IEnumerator suspensiveEnd()
    {
        yield return new WaitForSeconds(2f);
        audioManager.PlaySFX(audioManager.fullBreakSFX);
        currentSuspectState++;
        LevelData crime = crimes[currentCrimeIndex];
        UIManager.instance.ShowJudgePanel(crime.crimeOptions[currentCrimeQuestion], crime.suspectImg[currentSuspectState]);
        yield return new WaitForSeconds(2f);
        GoNextQuestion();
    }

}
