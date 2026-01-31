using System;
using TMPro;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public LevelData[] crimes;
    [SerializeField] private int currentCrimeIndex = 0;
    [SerializeField] private int currentCrimeQuestion = 0;

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

    void Start()
    {
        UIManager.instance.onAnswerSelected += ReceiveAnswer;
    }

    public void SetCurrentCrime(int index) => currentCrimeIndex = index;

    [ContextMenu("CreateCrimeOption")]
    public void CreateCrimeOptions()
    {
        LevelData crime = crimes[currentCrimeIndex];
        UIManager.instance.ShowJudgePanel(crime.crimeOptions[currentCrimeQuestion], crime.suspectImg);
    }

    public void ReceiveAnswer(int index)
    {
        if (CurrentCrimeOptions.options[index].correctPercentage > 0) OnReceiveCorrectAnswer();
        else OnReceiveWrongAnswer();
        
        UIManager.instance.FillSuspectPercentage(CurrentCrimeOptions.options[index].correctPercentage);
    }

    void OnReceiveCorrectAnswer()
    {
        cameraShake2D.Shake();
        uIShake.Shake();
        LeanTween.scaleX(imgSuspect, scale.x, durationX).setEase(ease).setLoopPingPong(1);
        LeanTween.scaleY(imgSuspect, scale.y, durationY).setEase(ease).setLoopPingPong(1);
    }
    [ContextMenu("AnimWrongAnswer")]
     void OnReceiveWrongAnswer()
    {
        LeanTween.scale(imgSuspect,scale * 1.15f,2).setEase(curveAnimWrongAnswer);
    }

}
