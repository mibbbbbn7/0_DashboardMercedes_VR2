using DashboardMercedes;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSpeaker : BaseMonoBehaviour<ISoundFeatureInternal>
{
    [SerializeField] private AudioSource _sfxSource;
    SoundFeature instance;

    protected Client _client;
    protected IBroadcaster _broadcaster;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        //_featureBroadcaster.Add<PlayOneShotEvent>(PlayOneShot);
        _featureBroadcaster.Add<ButtonHoldedNowStartEvent>(PlayCarEngine);

        _client = Client.Instance;
        _broadcaster = _client.Services.Get<IBroadcaster>();
        _broadcaster.Add<PlayOneShotEvent>(PlayOneShot);
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<PlayOneShotEvent>(PlayOneShot);
        _featureBroadcaster.Remove<ButtonHoldedNowStartEvent>(PlayCarEngine);
    }

    private void PlayOneShot(PlayOneShotEvent e)
    {
        Debug.Log("PlayOneShot");
        AudioClip clip = Resources.Load<AudioClip>("Audio/SFX/ButtonHover");
        //AudioClip clip = Resources.Load<AudioClip>("Audio/ButtonHover"); // Fixed: Replaced AudioResource.Load with Resources.Load
        _sfxSource.PlayOneShot(clip, 1);
    }

    private void PlayCarEngine(ButtonHoldedNowStartEvent e)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/ButtonHover"); // Fixed: Replaced AudioResource.Load with Resources.Load
        _sfxSource.PlayOneShot(clip);
    }
}
