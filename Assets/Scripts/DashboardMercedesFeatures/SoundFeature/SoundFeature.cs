using System.Threading.Tasks;
using DashboardMercedes;
using UnityEngine;
using UnityEngine.Audio;


public enum Channel
{
    Music,
    SFX
}

public class SoundFeature : BaseFeature, ISoundFeature, ISoundFeatureInternal, IInitializationController
{
    SoundFeatureData _featureData;

    private ILocalDataService _localDataService;

    public LocalSoundData GetLocalData => _featureData.MyLocalSoundData;

    public SoundFeature(Client client) : base(client)
    {
        
    }

    public void InitializeFeature()
    {
        _featureData = new SoundFeatureData();
        _localDataService = _client.Services.Get<ILocalDataService>();
    }

    public async Task LoadFeature()
    {
        await LoadLocalData();
        _featureData.MyMixer = await _assetService.Load<AudioMixer>(SoundFeatureData.AUDIO_MIXER_ASSET_PATH);

        _featureData.MyMixer.SetFloat("MusicVolume", _featureData.MyLocalSoundData.Music.Volume.Remap(new Vector2(0, 1), new Vector2(-80,0)));
        _featureData.MyMixer.SetFloat("SFXVolume", _featureData.MyLocalSoundData.SFX.Volume.Remap(new Vector2(0, 1), new Vector2(-80,0)));

        var soundSpeaker = await _assetService.InstantiateAsset<SoundSpeakerGB>(SoundFeatureData.SOUND_SPEAKER_ASSET_NAME);
        soundSpeaker.Initialize(this);
    }

    public void OnFeatureLoadedAndInitialized()
    {
        
    }

    private async Task LoadLocalData()
    {
        if (_localDataService.DoesFileExist(SoundFeatureData.LOCAL_FILE_NAME))
        {
            await _localDataService.LoadLocalData<LocalSoundData>(SoundFeatureData.LOCAL_FILE_NAME, OnLoadedFile);
        }
        else
        {
            SaveLocalFile();
        }
    }

    private void SaveLocalFile()
    {
        _localDataService.SaveLocalData(SoundFeatureData.LOCAL_FILE_NAME, _featureData.MyLocalSoundData, () => Debug.Log("Sound file saved!"));
    }

    private void OnLoadedFile(LocalSoundData data)
    {
        _featureData.MyLocalSoundData = data;
    }

    public void SaveSettings(float music, float sfx)
    {
        _featureData.MyLocalSoundData.Music.Volume = music;
        _featureData.MyLocalSoundData.SFX.Volume = sfx;

        SaveLocalFile();

        _featureData.MyMixer.SetFloat("MusicVolume", _featureData.MyLocalSoundData.Music.Volume.Remap(new Vector2(0, 1), new Vector2(-80, 0)));
        _featureData.MyMixer.SetFloat("SFXVolume", _featureData.MyLocalSoundData.SFX.Volume.Remap(new Vector2(0, 1), new Vector2(-80, 0)));
    }
}
