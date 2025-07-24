using DashboardMercedes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnCarBehaviour : BaseMonoBehaviour<ITurnOnCarFeatureInternal>
{
    [SerializeField] private RawImage _buttonBloom;
    [SerializeField] private Button _button;
    private Vector2 bloomScaleInit = Vector2.one;
    private Vector2 bloomScaleEnd = Vector2.one * 5.0f;
    private const float bloomDurationStep = 1.0f;
    Coroutine bloomCoroutine;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, bloomScaleInit, bloomScaleEnd));
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
    }

    private IEnumerator BloomAnimationStep(RectTransform bloomTransform, Vector2 startScale, Vector2 targetScale)
    {
        float time = 0f;

        while (time < bloomDurationStep)
        {
            float t = time / bloomDurationStep;
            t *= (2 - t);
            time += Time.deltaTime;
            bloomTransform.localScale = Vector2.Lerp(startScale, targetScale, t);
            yield return null;
        }

        bloomTransform.localScale = targetScale;
        //Debug.Log("snapped to scale");
        if (bloomScaleEnd == targetScale)
        {
            bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, bloomScaleEnd, bloomScaleInit));
        }
        else
        {
            bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, bloomScaleInit, bloomScaleEnd));
        }
    }
}