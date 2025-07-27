using System.Threading.Tasks;
using DashboardMercedes;


public class LoadingStartFeature : BaseFeature, ILoadingStartFeature, ILoadingStartFeatureInternal, IDashboardInitializationController
{
    private LoadingStartData _featureData;

    public LoadingStartFeature(Client client) : base(client)
    {
        _featureData = new();
    }
    public void InitializeFeature()
    {
        UnityEngine.Debug.Log("Initializing");
    }

    public async Task LoadFeature()
    {
        // Simulating an asynchronou
        await Task.Delay(1);
    }

    public void OnFeatureLoadedAndInitialized()
    {

    }

    public async Task InstantiateLoadingStart()
    {
        var loadingStartInstance = await _assetService.InstantiateAsset<LoadingStartBehaviour>(LoadingStartData.LOADING_START_PREFAB_PATH);
        loadingStartInstance.Initialize(this);
    }
}
