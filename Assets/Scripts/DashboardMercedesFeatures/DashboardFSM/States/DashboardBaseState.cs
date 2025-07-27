namespace DashboardMercedes
{
    public abstract class DashboardBaseState : IState
    {
        protected DashboardStateContext _context;

        public DashboardBaseState(DashboardStateContext context)
        {
            _context = context;
        }

        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
    }
}
