using DashboardMercedes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButtonBehaviour : BaseSelfInjectedBehaviour<ITurnOnCarFeatureInternal, ITurnOnCarFeature>, IPointerDownHandler, IPointerUpHandler
{
    private Button _button;

    private bool _isHolding = false;
    private float _holdTime = 0f;
    private float _requiredHoldTime = 3f;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _button = GetComponent<Button>();
        if (_button == null)
        {
            Debug.Log("Button not attached");
        } 
    }

    private void Update()
    {
        if (_isHolding)
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= _requiredHoldTime)
            {
                _isHolding = false;
                _holdTime = 0f;
                Debug.Log("Car started!");
                _featureBroadcaster.Broadcast(new ButtonHoldedNowStartEvent());
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
        _holdTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
        _holdTime = 0f;
    }
}
