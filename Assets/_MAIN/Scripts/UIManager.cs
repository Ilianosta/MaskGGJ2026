using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {
        if (UIManager.instance != null) Destroy(gameObject);
        else UIManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ShowJudgePanel(CrimeOptions crimeOptions)
    {

    }
}
