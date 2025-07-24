using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardMercedes;

public class InitializationController : IInitializationController
{
    private List<IInitializationController> _myFeatures = new();

    public InitializationController(Client client)
    {
        var allFeatures = client.Features.GetAll();

        foreach (var feature in allFeatures)
        {
            if(feature is IInitializationController initializedFeature)
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
