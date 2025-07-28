using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DashboardMercedes
{
    public class MenuState: DashboardBaseState
    {
        private List<GameObject> _menuPages;
        private GameObject _dotsContainer;
        private GameObject _dotPrefab;
        private int _currentPageIndex;
        private RectTransform _swipableArea;
        private float _swipeThreshold = 200f;
        private Vector2 _touchStartingPos;

        public MenuState(DashboardStateContext context) : base(context)
        {
            _menuPages = _context.MyDashboard._menuPages;
            _dotsContainer = _context.MyDashboard._dotsContainer;
            _dotPrefab = _context.MyDashboard._dotPrefab;
            _currentPageIndex = _context.MyDashboard._currentPageIndex;
            _swipableArea = _context.MyDashboard._swipableArea;
        }

        public override void StateOnEnter()
        {
            ShowContent();//DA RIVEDERE NOMI E ANIMAZIONE
        }

        public override void StateOnExit()
        {
        }

        public override void StateOnUpdate()
        {
            DetectSwipe();
        }

        public void ShowContent()
        {
            for (int i = 0; i < _menuPages.Count; i++)
            {
                bool isActive; //visibilita pagina corretta
                if (i == _currentPageIndex)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
                _menuPages[i].SetActive(isActive);

                Image dotImage = _dotsContainer.transform.GetChild(i).GetComponent<Image>();
                dotImage.color = isActive ? Color.white : Color.gray;

                dotImage.fillAmount = isActive ? 1f : 0f; //-------------------------------------------
            }
        }

        public void DetectSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _touchStartingPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 touchEndPos = Input.mousePosition;
                float swipeDistance = touchEndPos.x - _touchStartingPos.x;

                if (Mathf.Abs(swipeDistance) > _swipeThreshold && IsTouchInContentArea(_touchStartingPos))
                {
                    if ((_currentPageIndex == 0 && swipeDistance > 0) || (_currentPageIndex == _menuPages.Count - 1 && swipeDistance < 0))
                    {
                        return;
                    }

                    if (swipeDistance > 0)
                    {
                        PreviousContent();
                    }
                    else
                    {
                        NextContent();
                    }
                }
            }
        }

        public bool IsTouchInContentArea(Vector2 touchPosition)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_swipableArea, touchPosition);
        }
        void NextContent()
        {
            _currentPageIndex = _currentPageIndex + 1;
            ShowContent();
            UpdateDots();
        }

        void PreviousContent()
        {
            _currentPageIndex = _currentPageIndex - 1;
            ShowContent();
            UpdateDots();
        }
        void UpdateDots()
        {
            for (int i = 0; i < _dotsContainer.transform.childCount; i++)
            {
                Image dotImage = _dotsContainer.transform.GetChild(i).GetComponent<Image>();
                dotImage.color = (i == _currentPageIndex) ? Color.white : Color.gray;
            }
        }
    }
}