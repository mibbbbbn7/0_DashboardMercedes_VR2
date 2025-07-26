namespace DashboardMercedes
{
    public class TurnOnCarClient : Client
    {
        public override void InitFeatures()
        {
            Features.Add<ISoundFeature>(new SoundFeature(this));
            Features.Add<ITurnOnCarFeature>(new TurnOnCarFeature(this));
        }

        public override void InitControllers()
        {
            Controllers.Add<ITurnOnCarInitializationController>(new TurnOnCarInitializationController(this));
        }

        public override void StartClient()
        {
            CarStartUpFlow myFlow = new CarStartUpFlow();
            myFlow.BeginStartUp(this);
        }
    }
}
