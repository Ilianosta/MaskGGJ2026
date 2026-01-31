using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float maxTime;
    public float MaxTime => maxTime;
    float currentTime;
    public float CurrentTime => currentTime;
    bool active;
    public bool IsWorking => active;

    // Events
    public delegate void OnTimerEnd();
    public OnTimerEnd onTimerEnd;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        if (!active) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            active = false;
            currentTime = 0;
            onTimerEnd?.Invoke();
        }
    }

    [ContextMenu("Enable timer")]
    public void EnableTimer()
    {
        active = true;
    }
    
    [ContextMenu("Disable timer")]
    public void DisableTimer()
    {
        active = false;
    }

    [ContextMenu("Reset timer")]
    public void ResetTimer()
    {
        active = false;
        currentTime = maxTime;
    }
}
