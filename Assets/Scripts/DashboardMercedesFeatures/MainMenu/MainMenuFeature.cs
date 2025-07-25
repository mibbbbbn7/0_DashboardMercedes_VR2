using System.Threading.Tasks;
using DashboardMercedes;
using UnityEngine;

public class MainMenuFeature : BaseFeature, IMainMenuFeature, IMainMenuFeatureInternal, IDashboardInitializationController
{
    private MainMenuFeatureData _featureData;

    public MainMenuFeature(Client client) : base(client)
    {
        _featureData = new();
    }

    public async Task InstantiateMainMenu()
    {
        var mainMenu = await _assetService.InstantiateAsset<MainMenuBehaviour>(MainMenuFeatureData.MAIN_MENU_ASSET_NAME);
        mainMenu.Initialize(this);
    }

    public void InitializeFeature()
    {
        UnityEngine.Debug.Log("Initializing");
    }

    public Task LoadFeature()
    {
        UnityEngine.Debug.Log("Loading");

        return Task.CompletedTask;
    }

    public void OnFeatureLoadedAndInitialized()
    {
        UnityEngine.Debug.Log("Initialized and loaded");

    }
}
