using DashboardMercedes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardBehaviour : BaseMonoBehaviour<IDashboardFeatureInternal>
{
    private DashboardFSM _dashboardStateMachine;

    [SerializeField] public List<GameObject> _menuPages;
    [SerializeField] public GameObject _dotsContainer;
    [SerializeField] public GameObject _dotPrefab;
    [SerializeField] public RectTransform _swipableArea;
    
    public int _currentPageIndex = 0;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();
        _dashboardStateMachine = new DashboardFSM();

        DashboardStateContext context = new DashboardStateContext()
        {
            Client = _feature.GetClient(),
            MyDashboard = this,
            DashboardStateMachine = _dashboardStateMachine
        };

        //InitializeDots();

        // AGGIUNGI GLI ADD STATE ============================================================================
        _dashboardStateMachine.AddState(DashboardData.MENU_STATE, new MenuState(context));
        _dashboardStateMachine.AddState(DashboardData.MAIN_MENU_STATE, new MainMenuState(context));
        _dashboardStateMachine.AddState(DashboardData.APP_STATE, new AppState(context));

        //_dashboardStateMachine.GoTo(DashboardData.MENU_STATE);
        Debug.Log("current state:" + _dashboardStateMachine.GetCurrentState());
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
        _dashboardStateMachine.UpdateState();
    }

    protected override void ManagedStart()
    {
        base.ManagedStart();

        InitializeDots();
        _dashboardStateMachine.GoTo(DashboardData.MENU_STATE);
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();
    }

    protected void InitializeDots()
    {
        for (int i = 0; i < _menuPages.Count; i++)
        {
            GameObject dot = Instantiate(_dotPrefab, _dotsContainer.transform);
            Image dotImage = dot.GetComponent<Image>();
            if (i == _currentPageIndex)
            {
                dotImage.color = Color.white;
            }else
            {
                dotImage.color = Color.gray;
            }
            
            dotImage.fillAmount = 0f;
        }
    }
}