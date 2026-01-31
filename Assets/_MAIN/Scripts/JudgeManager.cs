using TMPro;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public LevelData[] crimes;
    [SerializeField] private int currentCrimeIndex = 0;
    [SerializeField] private int currentCrimeQuestion = 0;

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
        Debug.Log("Answer correct percentage: " + crimes[currentCrimeIndex].crimeOptions[currentCrimeQuestion].options[index].correctPercentage);
    }
}
