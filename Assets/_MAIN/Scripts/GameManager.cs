using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else GameManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.instance.suspectTimer.onTimerEnd += OnTimerEnd;
    }

    void OnTimerEnd()
    {
        Debug.Log("TIME OUT!");   
    }
}
