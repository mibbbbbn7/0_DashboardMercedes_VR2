namespace DashboardMercedes
{
    public abstract class DashboardBaseState : IState
    {
        protected DashboardStateContext _context;

        public DashboardBaseState(DashboardStateContext context)
        {
            _context = context;
        }

        public abstract void StateOnEnter();

        public abstract void StateOnExit();

        public abstract void StateOnUpdate();
    }
}
