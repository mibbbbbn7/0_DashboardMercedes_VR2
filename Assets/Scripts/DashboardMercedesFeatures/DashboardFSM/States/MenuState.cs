using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DashboardMercedes
{
    public class MenuState : DashboardBaseState
    {
        IBroadcaster clientBroadcaster;
        // forse era meglio riempire il context BOOOOOO
        private List<GameObject> _menuPages;
        private GameObject _dotsContainer;
        private int _currentPageIndex;
        private int _previousPageIndex;
        private RectTransform _swipableArea;
        private float _swipeThreshold = 200f;
        private Vector2 _touchStartingPos;
        private Button _climaButton;
        private GameObject _climaAppObject;
        private GameObject mainMenuObj;

        // 4 Anim
        private float _pageWidth;
        private RectTransform _pagesContainer;
        private bool _isAnimating = false;
        private float _animationProgress = 0f;
        private float _transitionSpeed = 10f;
        private AnimationCurve _transitionCurve;

        public MenuState(DashboardStateContext context) : base(context)
        {
            clientBroadcaster = _context.Client.Services.Get<IBroadcaster>();

            _menuPages = _context.MyDashboard._menuPages;
            _dotsContainer = _context.MyDashboard._dotsContainer;
            _currentPageIndex = _context.MyDashboard._currentPageIndex;
            _swipableArea = _context.MyDashboard._swipableArea;
            _pagesContainer = _context.MyDashboard._pagesContainer;
            _climaButton = _context.MyDashboard._climaButton;

            mainMenuObj = _context.mainMenuObj;
            _climaAppObject = _context.climaAppObj;

            _transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            _previousPageIndex = _currentPageIndex;

            if (_menuPages.Count > 0)
            {
                RectTransform firstPageRect = _menuPages[0].GetComponent<RectTransform>();
                _pageWidth = firstPageRect.rect.width;
            }

            _climaButton.onClick.AddListener(GoToClimaState);
        }

        public void GoToClimaState()
        {
            _climaAppObject.SetActive(true);

            clientBroadcaster.Broadcast(new PlayClickSoundEvent());
            _context.DashboardStateMachine.GoTo(DashboardData.APP_STATE);
        }

        public override void StateOnEnter()
        {
            _currentPageIndex = _context.MyDashboard.getIndex();
            AnimateToCurrentPage();
            AnimatePageTransition();
            mainMenuObj.SetActive(true);
        }

        public override void StateOnExit()
        {
            _context.MyDashboard.setIndex(_currentPageIndex);
            mainMenuObj.SetActive(false);
        }

        public override void StateOnUpdate()
        {
            DetectSwipe();

            if (_isAnimating)
            {
                AnimatePageTransition();
            }
        }


        private void AnimatePageTransition()
        {
            _animationProgress += Time.deltaTime * _transitionSpeed;

            float easedProgress = _transitionCurve.Evaluate(_animationProgress);

            Vector2 startPosition = new Vector2(-_previousPageIndex * _pageWidth - 960, 540);
            Vector2 endPosition = new Vector2(-_currentPageIndex * _pageWidth - 960, 540);

            _pagesContainer.localPosition = Vector2.Lerp(startPosition, endPosition, easedProgress);

            AnimateDotsTransition(easedProgress);

            if (_animationProgress >= 1f)
            {
                _isAnimating = false;
                _animationProgress = 0f;
                _pagesContainer.localPosition = endPosition;
            }
        }

        private void AnimateDotsTransition(float progress)
        {
            for (int i = 0; i < _dotsContainer.transform.childCount; i++)
            {
                Image dotImage = _dotsContainer.transform.GetChild(i).GetComponent<Image>();

                if (i == _previousPageIndex)
                {
                    dotImage.color = Color.Lerp(Color.white, Color.gray, progress);
                }
                else if (i == _currentPageIndex)
                {
                    dotImage.color = Color.Lerp(Color.gray, Color.white, progress);
                }
            }
        }

        public void DetectSwipe()
        {
            if (_isAnimating) return;

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
                    { // Block extremes
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
            if (_isAnimating) return;

            _previousPageIndex = _currentPageIndex;
            _currentPageIndex = _currentPageIndex + 1;
            AnimateToCurrentPage();

            _context.MyDashboard._currentPageIndex = _currentPageIndex; // For back button
        }

        void PreviousContent()
        {
            if (_isAnimating) return;

            _previousPageIndex = _currentPageIndex;
            _currentPageIndex = _currentPageIndex - 1;
            AnimateToCurrentPage();

            _context.MyDashboard._currentPageIndex = _currentPageIndex;
        }

        private void AnimateToCurrentPage()
        {
            _isAnimating = true;
            _animationProgress = 0f;
        }
    }
}