using System.Threading.Tasks;
using UnityEngine;

public interface ITurnOnCarInitializationController : IController
{
    public void InitializeFeature();

    public Task LoadFeature();

    public void OnFeatureLoadedAndInitialized();
}