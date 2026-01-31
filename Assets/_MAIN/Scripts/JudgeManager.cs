using TMPro;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public void CreateCrimeOptions(LevelData levelData, int index)
    {
        UIManager.instance.ShowJudgePanel(levelData.crimeOptions[index]);
    }
}
