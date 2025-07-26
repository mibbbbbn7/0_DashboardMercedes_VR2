using DashboardMercedes;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundSpeaker : BaseMonoBehaviour<ISoundFeatureInternal>
{
    [SerializeField] private AudioSource _sfxSource;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _featureBroadcaster.Add<PlayOneShotEvent>(PlayOneShot);
        _featureBroadcaster.Add<ButtonHoldedNowStartEvent>(PlayCarEngine);
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<PlayOneShotEvent>(PlayOneShot);
        _featureBroadcaster.Remove<ButtonHoldedNowStartEvent>(PlayCarEngine);
    }

    private void Play(Channel channel, SFXInfoSO info)
    {
        _sfxSource.clip = info.Clip;
        _sfxSource.volume = info.Volume;
        _sfxSource.Play();
    }

    private void PlayOneShot(PlayOneShotEvent e)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/ButtonHover");
        _sfxSource.PlayOneShot(clip);
    }

    private void PlayCarEngine(ButtonHoldedNowStartEvent e)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/ButtonHover");
        _sfxSource.PlayOneShot(clip);
    }
}
