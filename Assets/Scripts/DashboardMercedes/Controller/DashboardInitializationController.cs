using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardMercedes;

public class DashboardInitializationController : IDashboardInitializationController
{
    private List<IDashboardInitializationController> _myFeatures = new();

    public DashboardInitializationController(Client client)
    {
        var allFeatures = client.Features.GetAll();

        foreach (var feature in allFeatures)
        {
            if(feature is IDashboardInitializationController initializedFeature)
            {
                _myFeatures.Add(initializedFeature);
            }
        }
    }

    public void InitializeFeature()
    {
        foreach (var feature in _myFeatures)
        {
            feature.InitializeFeature();
        }
    }

    public async Task LoadFeature()
    {
        List<Task> mytasks = new();
        foreach (var feature in _myFeatures)
        {
            mytasks.Add(feature.LoadFeature());
        }

        await Task.WhenAll(mytasks);
    }

    public void OnFeatureLoadedAndInitialized()
    {
        foreach (var feature in _myFeatures)
        {
            feature.OnFeatureLoadedAndInitialized();
        }
    }
}
