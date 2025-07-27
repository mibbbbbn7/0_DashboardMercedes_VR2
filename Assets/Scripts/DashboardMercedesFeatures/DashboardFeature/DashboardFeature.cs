using System.Threading.Tasks;
using DashboardMercedes;


public class DashboardFeature : BaseFeature, IDashboardFeature, IDashboardFeatureInternal, IDashboardInitializationController
{
    private DashboardData _featureData;

    public DashboardFeature(Client client) : base(client)
    {
        _featureData = new();
    }

    public void InitializeFeature()
    {
    }

    public async Task LoadFeature()
    {
        await Task.Delay(1);
    }

    public void OnFeatureLoadedAndInitialized()
    {

    }

    public async Task InstantiateDashboardFeature()
    {
        var DashboardInstance = await _assetService.InstantiateAsset<DashboardBehaviour>(DashboardData.DASHBOARD_PREFAB_PATH);
        DashboardInstance.Initialize(this);
    }

    public Client GetClient()
    {
        return _client;
    }
}
