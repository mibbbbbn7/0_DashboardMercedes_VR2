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
            Controllers.Add<IInitializationController>(new InitializationController(this));
        }

        public override void StartClient()
        {
            _broadcaster.Broadcast(new LoadFeaturesEvent());
            StartUpFlow myFlow = new StartUpFlow();
            myFlow.BeginStartUp(this);
        }
    }
}
