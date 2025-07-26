using System.Threading.Tasks;
using DashboardMercedes;


public class TurnOnCarFeature : BaseFeature, ITurnOnCarFeature, ITurnOnCarFeatureInternal, ITurnOnCarInitializationController
{
    private TurnOnCarData _featureData;

    public TurnOnCarFeature(Client client) : base(client)
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
        UnityEngine.Debug.Log("Initializing");
    }

    public async Task InstantiateTurnOnCarFeature()
    {
        var turnOnCarInstance = await _assetService.InstantiateAsset<TurnOnCarBehaviour>(TurnOnCarData.TURN_ON_CAR_PREFAB_PATH);
        turnOnCarInstance.Initialize(this);
    }
}
