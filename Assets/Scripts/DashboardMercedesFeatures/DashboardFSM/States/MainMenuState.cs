namespace DashboardMercedes
{
    public class MainMenuState : DashboardBaseState
    {
        public MainMenuState(DashboardStateContext context) : base(context)
        {
        }

        public override void StateOnEnter()
        {
            _context._currentPageIndex = 0;
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