using UnityEngine;

namespace DashboardMercedes
{
    public abstract class Client : MonoBehaviour
    {
        public static Client Instance { get; private set; }

        public Locator<IService> Services;
        public Locator<IFeature> Features;
        public Locator<IController> Controllers;

        protected IBroadcaster _broadcaster;

        public abstract void InitFeatures();
        public abstract void InitControllers();
        public void InitServices()
        {
            Services.Add<IAssetService>(new AssetService());
            Services.Add<IBroadcaster>(new Broadcaster());
            Services.Add<ILocalDataService>(new LocalDataService());
        }

        public abstract void StartClient();

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            Services = new Locator<IService>();
            Features = new Locator<IFeature>();
            Controllers = new Locator<IController>();

            InitServices();
            InitFeatures();
            InitControllers();

            _broadcaster = Services.Get<IBroadcaster>();//?????????????perche la chiamata dopo si fermmma??????????????????
        }

        private void Start()
        {
            StartClient();
        }

        private void OnDestroy()
        {
            // cleanup features
        }

    }
}