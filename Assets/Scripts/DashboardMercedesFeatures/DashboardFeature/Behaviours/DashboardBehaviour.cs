using DashboardMercedes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashboardBehaviour : BaseMonoBehaviour<IDashboardFeatureInternal>
{
    private DashboardFSM _dashboardStateMachine;

    [SerializeField] public List<GameObject> _menuPages;
    [SerializeField] public GameObject _dotsContainer;
    [SerializeField] public GameObject _dotPrefab;
    [SerializeField] public RectTransform _swipableArea;
    [SerializeField] public RectTransform _pagesContainer;
    [SerializeField] public Button _climaButton;

    [SerializeField] public GameObject mainMenuObj;
    [SerializeField] public GameObject climaAppObj;


    //clima
    [SerializeField] public RawImage ClimaColorBack;
    [SerializeField] public Button Hotter;
    [SerializeField] public Button Colder;
    [SerializeField] public Button Stronger;
    [SerializeField] public Button Weaker;

    [SerializeField] public Button Back;
    [SerializeField] public Button Home;

    [SerializeField] public TextMeshProUGUI Text;

    public int _currentPageIndex = 0;
    public int getCurrentIndex()
    {
        Debug.Log(_currentPageIndex);
        return _currentPageIndex;

    }
    public void setCurrentIndex(int indexNew)
    {
        _currentPageIndex = indexNew;
        Debug.Log(_currentPageIndex);
    }

    public int _previousPageIndex = 1;
    public int getPreviousIndex()
    {
        Debug.Log(_previousPageIndex);
        return _previousPageIndex;

    }
    public void setPreviousIndex(int indexNew)
    {
        _previousPageIndex = indexNew;
        Debug.Log(_currentPageIndex);
    }

    protected override void ManagedAwake()
    {
        base.ManagedAwake();
        _dashboardStateMachine = new DashboardFSM();

        DashboardStateContext context = new DashboardStateContext()
        {
            Client = _feature.GetClient(),
            MyDashboard = this,
            DashboardStateMachine = _dashboardStateMachine,
            mainMenuObj = mainMenuObj,
            climaAppObj = climaAppObj
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