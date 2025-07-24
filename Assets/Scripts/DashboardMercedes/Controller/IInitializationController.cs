using System.Threading.Tasks;
using UnityEngine;

public interface IInitializationController : IController
{
    public void InitializeFeature();

    public Task LoadFeature();

    public void OnFeatureLoadedAndInitialized();
}
