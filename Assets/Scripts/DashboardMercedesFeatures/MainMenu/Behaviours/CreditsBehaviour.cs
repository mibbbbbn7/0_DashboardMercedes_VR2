using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditsBehaviour : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _myCredits;
    [SerializeField] private float _scrollSpeed;

    private bool _isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private void OnEnable()
    {
        _myCredits.verticalNormalizedPosition = 1;
    }

    private void Update()
    {
        if (_isDragging) return;

        _myCredits.verticalNormalizedPosition -= Time.deltaTime * _scrollSpeed;

        if(_myCredits.verticalNormalizedPosition < 0)
        {
            _myCredits.verticalNormalizedPosition = 1;
        }
    }
}
