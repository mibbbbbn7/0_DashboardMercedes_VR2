using DashboardMercedes;
using Unity.VisualScripting;

public class DashboardBehaviour : BaseMonoBehaviour<IDashboardFeatureInternal>
{
    private DashboardFSM _dashboardStateMachine;

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

        // AGGIUNGI GLI ADD STATE ============================================================================
        _dashboardStateMachine.AddState(DashboardData.LOADING_STATE, new LoadingState(context));
        _dashboardStateMachine.AddState(DashboardData.MENU_STATE, new MenuState(context));
        _dashboardStateMachine.AddState(DashboardData.MAIN_MENU_STATE, new MainMenuState(context));
        _dashboardStateMachine.AddState(DashboardData.APP_STATE, new AppState(context));

        _dashboardStateMachine.GoTo(DashboardData.LOADING_STATE);
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
        _dashboardStateMachine.UpdateState();
    }

    protected override void ManagedStart()
    {
        base.ManagedStart();
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();
    }
}