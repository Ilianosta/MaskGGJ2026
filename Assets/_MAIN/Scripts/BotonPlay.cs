using UnityEngine;

public class BotonPlay : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    [SerializeField] private RectTransform referenceObject1;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;

    [SerializeField] private float timerChoiceTotal;
    [SerializeField] private float timerCurrentTime;

    private void Update()
    {
        Rect rectA = GetWorldRect(selector);
        Rect rectB = GetWorldRect(referenceObject1);

        if (rectA.Overlaps(rectB))
        {
            if (timerCurrentTime >= timerChoiceTotal)
            {
                timerCurrentTime = 0;
                gameMenu.SetActive(true);
                mainMenu.SetActive(false);
            }
            else
                timerCurrentTime++;
        }
        else if (timerCurrentTime >= 0)
            timerCurrentTime--;
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
