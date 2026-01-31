using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject firstScene, interrogatory;
    public Timer firstSceneTimer;
    public JudgeManager judgeManager;

    public delegate void OnGameStart();
    public OnGameStart onGameStart;
    public delegate void OnGameFinish();
    public OnGameFinish onGameFinish;
    public bool gameStarted = false;
    void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else GameManager.instance = this;

        DontDestroyOnLoad(gameObject);

        SetFirstScene();
    }

    void Start()
    {
        firstSceneTimer.onTimerEnd += OnFirstSceneTimerEnd;
        DialogueManager.onFinishDialogue += () =>
        {
            if (gameStarted) return;
            gameStarted = true;
            onGameStart?.Invoke();
            judgeManager.CreateCrimeOptions();
            firstSceneTimer.EnableTimer();
            UIManager.instance.ShowBlackScreen(false);
        };

        DialogueManager.instance.StartDialogue();
    }

    void SetFirstScene()
    {
        firstScene.SetActive(true);
        interrogatory.SetActive(false);
    }

    private void OnFirstSceneTimerEnd()
    {
        firstScene.SetActive(false);
        interrogatory.SetActive(true);
        firstSceneTimer.DisableTimer();
        UIManager.instance.ShowInterrogatory(true);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER WACHO JAJAAAAA");
        UIManager.instance.onFinish = true;
        UIManager.instance.ShowInterrogatory(false);
    }
}
