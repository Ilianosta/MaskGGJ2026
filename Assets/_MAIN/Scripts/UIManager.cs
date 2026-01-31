using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] UI_JudgePanel judgePanel;

    public delegate void OnAnswerSelected(int index);
    public OnAnswerSelected onAnswerSelected;
    void Awake()
    {
        if (UIManager.instance != null) Destroy(gameObject);
        else UIManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ShowJudgePanel(CrimeOptions crime, Sprite suspect) => judgePanel.FillPanel(crime, suspect);
}