using UnityEngine;
using System.Collections;

public class UIShake : MonoBehaviour
{
    RectTransform rt;
    Vector2 originalPos;
    Coroutine routine;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        originalPos = rt.anchoredPosition;
    }

    public void Shake(float duration = 0.15f, float magnitude = 10f)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;
        originalPos = rt.anchoredPosition;

        while (elapsed < duration)
        {
            float t = 1f - (elapsed / duration);
            float x = Random.Range(-1f, 1f) * magnitude * t;
            float y = Random.Range(-1f, 1f) * magnitude * t;

            rt.anchoredPosition = originalPos + new Vector2(x, y);

            elapsed += Time.unscaledDeltaTime; // UI suele ir mejor sin scale
            yield return null;
        }

        rt.anchoredPosition = originalPos;
        routine = null;
    }
}
