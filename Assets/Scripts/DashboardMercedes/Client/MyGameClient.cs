namespace DashboardMercedes
{
    public class MyGameClient : Client
    {
        public override void InitFeatures()
        {
            Features.Add<ISoundFeature>(new SoundFeature(this));
            Features.Add<IMainMenuFeature>(new MainMenuFeature(this));
            Features.Add<ITurnOnCarFeature>(new TurnOnCarFeature(this));
        }

        public override void InitControllers()
        {
            Controllers.Add<IDashboardInitializationController>(new DashboardInitializationController(this));
            Controllers.Add<ITurnOnCarInitializationController>(new TurnOnCarInitializationController(this));
        }

        public override void StartClient()
        {
            StartUpFlow myFlow = new StartUpFlow();
            myFlow.BeginStartUp(this);
        }
    }
}
