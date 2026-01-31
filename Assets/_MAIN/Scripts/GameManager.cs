using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject firstScene, paper;
    public Timer firstSceneTimer;
    void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else GameManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.instance.suspectTimer.onTimerEnd += OnInterrogationTimeEnd;
        firstSceneTimer.onTimerEnd += OnFirstSceneTimerEnd;
    }

    private void OnFirstSceneTimerEnd()
    {
        
    }

    void OnInterrogationTimeEnd()
    {
        Debug.Log("TIME OUT!");
    }
}
