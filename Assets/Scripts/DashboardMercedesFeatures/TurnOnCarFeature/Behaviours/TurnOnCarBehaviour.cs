using DashboardMercedes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TurnOnCarBehaviour : BaseMonoBehaviour<ITurnOnCarFeatureInternal>
{
    [SerializeField] private RawImage _buttonBloom;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _turnOnCarCanvasGroup;

    private Vector2 bloomScaleInit = Vector2.one;
    private Vector2 bloomScaleEnd = Vector2.one * 5.0f;
    private const float bloomDurationStep = 1.0f;
    Coroutine bloomCoroutine;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        if (_button != null)
        {
            _button.onClick.AddListener(() => DoOnClick());
            
        }
        bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, bloomScaleInit, bloomScaleEnd));

        _featureBroadcaster.Add<ButtonHoldedNowStartEvent>(StartNow);
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<ButtonHoldedNowStartEvent>(StartNow);
    }

    private void DoOnClick()
    {
        Debug.Log("bottone cliccato");
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

    private void StartNow(ButtonHoldedNowStartEvent e)
    {
        Debug.Log("Broadcasteeeeeeeeed");
    }
}