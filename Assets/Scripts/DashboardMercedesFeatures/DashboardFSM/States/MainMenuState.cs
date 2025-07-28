namespace DashboardMercedes
{
    public class MainMenuState : DashboardBaseState
    {
        public MainMenuState(DashboardStateContext context) : base(context)
        {
        }

        public override void StateOnEnter()
        {
            _context.MyDashboard.setIndex(0);
            _context.DashboardStateMachine.GoTo(DashboardData.MENU_STATE);
        }

        public override void StateOnExit()
        {

        }

        public override void StateOnUpdate()
        {
            
        }
    }
}