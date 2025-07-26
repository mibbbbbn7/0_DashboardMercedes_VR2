using System.Threading.Tasks;
using DashboardMercedes;


public class CarFSMFeature : BaseFeature, ICarFSMFeature, ICarFSMFeatureInternal, ITurnOnCarInitializationController
{
    private CarFSMData _featureData;

    public CarFSMFeature(Client client) : base(client)
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

    public async Task InstantiateCarFSMFeature()
    {
        var CarFSMInstance = await _assetService.InstantiateAsset<CarFSMBehaviour>(CarFSMData.CAR_FSM_PREFAB_PATH);
        CarFSMInstance.Initialize(this);
    }
}
