using System.Threading.Tasks;
using UnityEngine;

public interface IDashboardInitializationController : IController
{
    public void InitializeFeature();

    public Task LoadFeature();

    public void OnFeatureLoadedAndInitialized();
}
