using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject firstScene, interrogatory;
    public Timer firstSceneTimer;
    void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else GameManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        firstSceneTimer.onTimerEnd += OnFirstSceneTimerEnd;
    }

    private void OnFirstSceneTimerEnd()
    {
        firstScene.SetActive(false);
        interrogatory.SetActive(true);
    }

    void OnInterrogationTimeEnd()
    {
        Debug.Log("TIME OUT!");
    }

    public void EndGame()
    {
        UIManager.instance.CloseJudgePanel();
        Debug.Log("GAME OVER WACHO JAJAAAAA");
    }
}
