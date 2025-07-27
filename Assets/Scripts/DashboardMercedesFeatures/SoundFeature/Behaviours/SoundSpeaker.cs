using DashboardMercedes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSpeaker : BaseMonoBehaviour<ISoundFeatureInternal>
{
    [SerializeField] private AudioSource _sfxSource;

    protected Client _client;
    protected IBroadcaster _broadcaster;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _client = Client.Instance;
        _broadcaster = _client.Services.Get<IBroadcaster>();
        _broadcaster.Add<PlayEngineSoundEvent>(PlayCarEngineSound);
        _broadcaster.Add<LoadingPlaySoundEvent>(PlayLoadingStartBeginSound);
    }

    protected override void ManagedUpdate()
    {
        base.ManagedUpdate();
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<PlayEngineSoundEvent>(PlayCarEngineSound);
    }

    private void PlayCarEngineSound(PlayEngineSoundEvent e) //questo lo faccio arrivare da TurnOnCarBehaviour
    {
        Debug.Log("PlayCarEngineSound");
        AudioClip clip = Resources.Load<AudioClip>("Audio/SFX/engineSound");
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    private void PlayLoadingStartBeginSound(LoadingPlaySoundEvent e)
    {
        Debug.Log("PlayLoadingStartBeginSound");
        AudioClip clip = Resources.Load<AudioClip>("Audio/SFX/turnOnSound");
        _sfxSource.clip = clip;
        _sfxSource.PlayOneShot(clip, 2f);
    }
}
