using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChoiceDetector : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    [SerializeField] private RectTransform referenceObject1;
    public int actualIndex;

    [SerializeField] private float timerChoiceTotal;
    [SerializeField] private float timerCurrentTime;

    [SerializeField] CursorEffect cursor;

    private void Update()
    {
        Rect rectA = GetWorldRect(selector);
        Rect rectB = GetWorldRect(referenceObject1);

        if (rectA.Overlaps(rectB))
        {
            cursor.AnimateIn();
            if (timerCurrentTime >= timerChoiceTotal)
            {
                UIManager.instance.onAnswerSelected?.Invoke(actualIndex);
                timerCurrentTime = 0;

            }
            else
                timerCurrentTime++;
        }
        else if (timerCurrentTime >= 0)
        {
            timerCurrentTime--;
            cursor.AnimateOut();
        }
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
                        corners[2].x - corners[0].x,
                        corners[2].y - corners[0].y);
    }
}