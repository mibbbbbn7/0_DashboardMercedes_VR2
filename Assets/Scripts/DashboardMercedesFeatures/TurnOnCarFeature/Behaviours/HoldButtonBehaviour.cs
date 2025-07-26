using DashboardMercedes;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButtonBehaviour : BaseSelfInjectedBehaviour<ITurnOnCarFeatureInternal, ITurnOnCarFeature>, IPointerDownHandler, IPointerUpHandler
{
    private Button _buttonComponent;
    private Image _buttonImageComponent;
    private RectTransform _buttonRectTransformComponent;

    private Vector2 _buttonScaleInit = Vector2.one;
    private Vector2 _buttonScaleEnd = Vector2.one * 0.97f;
    private Vector2 _buttonPositionInit = new Vector2(132.9618f, 147.048f);
    private Vector2 _buttonPositionEnd = new Vector2(131f, 145f);

    private const float _buttonDurationStep = 0.40f;
    Coroutine MoveScaleDimButtonCoroutine;

    private bool _isHolding = false;
    private float _holdTime = 0f;
    private const float _requiredHoldTimeToStart = 1f;

    protected override void ManagedAwake()
    {

        base.ManagedAwake();
        
        _buttonComponent = GetComponent<Button>();
        if (null == _buttonComponent)
        {
            Debug.Log("Button not attached");
        }

        _buttonImageComponent = GetComponent<Image>();
        if(null == _buttonImageComponent)
        {
            Debug.Log("Image not attached");
        }

        _buttonRectTransformComponent = GetComponent<RectTransform>();
        if (null == _buttonRectTransformComponent)
        {
            Debug.Log("RectTransform not attached");
        }

    }

    private void Update()
    {
        if (_isHolding)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= _requiredHoldTimeToStart)
            {
                _isHolding = false;
                _holdTime = 0f;
                _featureBroadcaster.Broadcast(new ButtonHoldedNowStartEvent());
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
        _holdTime = 0f;
        if(null != MoveScaleDimButtonCoroutine)
        {
            StopCoroutine(MoveScaleDimButtonCoroutine);
        }
        MoveScaleDimButtonCoroutine = StartCoroutine(MoveScaleButton(_buttonRectTransformComponent, _buttonRectTransformComponent.localScale, _buttonScaleEnd, _buttonRectTransformComponent.anchoredPosition, _buttonPositionEnd));
        _featureBroadcaster.Broadcast(new ButtonHoldingEvent());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
        _holdTime = 0f;
        if (null != MoveScaleDimButtonCoroutine)
        {
            StopCoroutine(MoveScaleDimButtonCoroutine);
        }
        MoveScaleDimButtonCoroutine = StartCoroutine(MoveScaleButton(_buttonRectTransformComponent, _buttonRectTransformComponent.localScale, _buttonScaleInit, _buttonRectTransformComponent.anchoredPosition, _buttonPositionInit));
        _featureBroadcaster.Broadcast(new ButtonReleaseEvent());
    }

    private IEnumerator MoveScaleButton(RectTransform buttonTransform, Vector2 startScale, Vector2 targetScale, Vector2 startX, Vector2 targetX)
    {
        float time = 0f;

        while (time < _buttonDurationStep)
        {
            float t = time / _buttonDurationStep;
            t *= (2 - t);
            time += Time.deltaTime;
            buttonTransform.localScale = Vector2.Lerp(startScale, targetScale, t);
            buttonTransform.anchoredPosition = Vector2.Lerp(startX, targetX, t);
            yield return null;
        }

        buttonTransform.localScale = targetScale;
        buttonTransform.anchoredPosition = targetX;
    }
}
