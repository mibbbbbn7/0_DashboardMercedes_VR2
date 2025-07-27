namespace DashboardMercedes
{
    public class DashboardClient : Client
    {
        public override void InitFeatures()
        {
            Features.Add<IDashboardFeature>(new DashboardFeature(this));
            Features.Add<ISoundFeature>(new SoundFeature(this));
            Features.Add<IMainMenuFeature>(new MainMenuFeature(this));
            Features.Add<ILoadingStartFeature>(new LoadingStartFeature(this));
        }

        public override void InitControllers()
        {
            Controllers.Add<IDashboardInitializationController>(new DashboardInitializationController(this));
        }

        public override void StartClient()
        {
            DashboardStartUpFlow myFlow = new DashboardStartUpFlow();
            myFlow.BeginStartUp(this);
        }
    }
}
