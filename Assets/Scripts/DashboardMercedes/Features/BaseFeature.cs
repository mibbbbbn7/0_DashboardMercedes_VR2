namespace DashboardMercedes {
    public class BaseFeature : IFeature, IFeatureInternal
    {
        protected Client _client;
        protected IBroadcaster _broadcaster;
        protected IAssetService _assetService;

        protected IBroadcaster _featureBroadcaster;

        public IBroadcaster FeatureBroadcaster => _featureBroadcaster;

        public BaseFeature(Client client)
        {
            _client = client;
            _broadcaster = client.Services.Get<IBroadcaster>();
            _assetService = client.Services.Get<IAssetService>();

            _featureBroadcaster = new Broadcaster();
        }
    }
}