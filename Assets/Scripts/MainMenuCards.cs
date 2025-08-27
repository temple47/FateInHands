using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform card;
    public CanvasGroup frontSide;
    public CanvasGroup backSide;
    public float duration = 0.25f;

    bool isFlipped;
    bool isAnimating;

    void Awake()
    {
        ShowFront();
        card.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFlipped && !isAnimating)
            StartCoroutine(FlipRoutine(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isFlipped && !isAnimating)
            StartCoroutine(FlipRoutine(false));
    }

    System.Collections.IEnumerator FlipRoutine(bool toFlipped)
    {
        isAnimating = true;
        float half = duration * 0.5f;

        // Shrink X
        yield return ScaleX(1f, 0f, half);

        // Swap
        if (toFlipped) ShowBack(); else ShowFront();

        // Expand X
        yield return ScaleX(0f, 1f, half);

        isFlipped = toFlipped;
        isAnimating = false;
    }

    System.Collections.IEnumerator ScaleX(float from, float to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(t / time);
            float x = Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, k));
            card.localScale = new Vector3(x, 1f, 1f);
            yield return null;
        }
        card.localScale = new Vector3(to, 1f, 1f);
    }

    void ShowFront()
    {
        SetGroup(frontSide, 1f, true);
        SetGroup(backSide, 0f, false);
    }

    void ShowBack()
    {
        SetGroup(frontSide, 0f, false);
        SetGroup(backSide, 1f, true);
    }

    static void SetGroup(CanvasGroup g, float alpha, bool interactable)
    {
        if (!g) return;
        g.alpha = alpha;
        g.interactable = interactable;
        g.blocksRaycasts = interactable;
    }
}


