using UnityEngine;
using UnityEngine.EventSystems;

public class CursorEffect : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easing;
    [SerializeField] Vector2 scale;

    Vector2 baseScale;

    void Awake()
    {
        baseScale = transform.localScale;
    }

    public void AnimateIn()
    {
        LeanTween.scale(gameObject, scale, duration).setEase(easing);
    }

    public void AnimateOut()
    {
        LeanTween.scale(gameObject, baseScale, duration).setEase(easing);
    }

}
