using System.Threading.Tasks;
using DashboardMercedes;
using UnityEngine;
using UnityEngine.Audio;


public class SoundFeature : BaseFeature, ISoundFeature, ISoundFeatureInternal, IDashboardInitializationController, ITurnOnCarInitializationController
{
    public SoundFeature(Client client) : base(client)
    {

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

    public async Task InstantiateSoundFeature()
    {
        var soundSpeaker = await _assetService.InstantiateAsset<SoundSpeaker>(SoundFeatureData.SOUND_SPEAKER_ASSET_NAME);
        soundSpeaker.Initialize(this);
    }
}
