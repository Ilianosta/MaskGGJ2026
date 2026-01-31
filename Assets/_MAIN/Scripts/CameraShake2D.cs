using UnityEngine;
using System.Collections;

public class CameraShake2D : MonoBehaviour
{
    Vector3 originalPos;
    Coroutine routine;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    public void Shake(float duration = 0.2f, float magnitude = 0.1f)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;
        originalPos = transform.localPosition;

        while (elapsed < duration)
        {
            float t = 1f - (elapsed / duration); // decae hacia 0 (más natural)
            float x = Random.Range(-1f, 1f) * magnitude * t;
            float y = Random.Range(-1f, 1f) * magnitude * t;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        routine = null;
    }
}
