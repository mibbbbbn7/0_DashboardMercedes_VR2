using System.Threading.Tasks;
using DashboardMercedes;


public class TurnOnCarFeature : BaseFeature, ITurnOnCarFeature, ITurnOnCarFeatureInternal, ITurnOnCarInitializationController
{
    private TurnOnCarData _featureData;

    public TurnOnCarFeature(Client client) : base(client)
    {
        _featureData = new();
    }
    public async Task InstantiateTurnOnCar()
    {
        var turnOnCarInstance = await _assetService.InstantiateAsset<TurnOnCarBehaviour>(TurnOnCarData.TURN_ON_CAR_PREFAB_PATH);
        turnOnCarInstance.Initialize(this);
    }

    public void InitializeFeature()
    {
        UnityEngine.Debug.Log("Initializing");
    }

    public Task LoadFeature()
    {
        return Task.CompletedTask;
    }

    public void OnFeatureLoadedAndInitialized()
    {
        UnityEngine.Debug.Log("Initializing");
    }
}
