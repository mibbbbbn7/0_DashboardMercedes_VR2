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
    [SerializeField] private Slider _sliderProgress;  //-https://www.youtube.com/watch?v=AyuQXfgVk3U

    private Vector2 bloomScaleInit = Vector2.one;
    private Vector2 bloomScaleEnd = Vector2.one * 3.0f;
    private const float bloomDurationStep = 1.0f;

    private const float sliderValueInit = 0f;

    Coroutine bloomCoroutine;
    Coroutine bloomHoldingCoroutine;
    Coroutine sliderProgressCoroutine;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        //if (_button != null)
        //{
        //    _button.onClick.AddListener(() => DoOnClick());
        //}

        bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, bloomScaleInit, bloomScaleEnd));

        _featureBroadcaster.Add<ButtonHoldedNowStartEvent>(StartNow);
        _featureBroadcaster.Add<ButtonHoldingEvent>(FreezeBloomOnHold);
        _featureBroadcaster.Add<ButtonReleaseEvent>(ButtonReleased);

        _sliderProgress.value = sliderValueInit;
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<ButtonHoldedNowStartEvent>(StartNow);
        _featureBroadcaster.Remove<ButtonHoldingEvent>(FreezeBloomOnHold);
        _featureBroadcaster.Remove<ButtonReleaseEvent>(ButtonReleased);
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

    private void FreezeBloomOnHold(ButtonHoldingEvent e)
    {
        if(null != bloomCoroutine)
        {
            StopCoroutine(bloomCoroutine);
        }

        bloomHoldingCoroutine = StartCoroutine(BloomAnimationHolding(_buttonBloom.rectTransform, _buttonBloom.rectTransform.localScale, bloomScaleInit));
        sliderProgressCoroutine = StartCoroutine(SliderProgressHolding(_sliderProgress));
    }

    private IEnumerator BloomAnimationHolding(RectTransform bloomTransform, Vector2 startScale, Vector2 targetScale)
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
    }

    private IEnumerator SliderProgressHolding(Slider sliderProgress)
    {
        float time = 0f;
        const float requiredHoldTimeToStart = 1f;

        while (time < requiredHoldTimeToStart)
        {
            float t = time / requiredHoldTimeToStart;
            //t *= (2 - t);
            time += Time.deltaTime;
            sliderProgress.value = time / requiredHoldTimeToStart;
            yield return null;
        }

        sliderProgress.value = 1f;
    }

    private void ButtonReleased(ButtonReleaseEvent e)
    {
        if(null != sliderProgressCoroutine)
        {
            StopCoroutine(sliderProgressCoroutine);
        }
        if(null != bloomHoldingCoroutine)
        {
            StopCoroutine(bloomHoldingCoroutine);
        }

        _sliderProgress.value = 0f;
        bloomCoroutine = StartCoroutine(BloomAnimationStep(_buttonBloom.rectTransform, _buttonBloom.rectTransform.localScale, bloomScaleInit));
    }

    private void StartNow(ButtonHoldedNowStartEvent e)
    {
        Debug.Log("Broadcasteeeeeeeeed");
    }
}