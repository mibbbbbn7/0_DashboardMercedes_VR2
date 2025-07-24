using DashboardMercedes;
using UnityEngine;

public class SoundSpeakerGB : BaseMonoBehaviour<ISoundFeatureInternal>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _featureBroadcaster.Add<PlayOneShotEvent>(PlayOneShot);
    }

    protected override void ManagedOnDestroy()
    {
        base.ManagedOnDestroy();

        _featureBroadcaster.Remove<PlayOneShotEvent>(PlayOneShot);
    }

    private void Play(Channel channel, SFXInfoSO info)
    {
        switch (channel)
        {
            case Channel.Music:
                _musicSource.clip = info.Clip;
                _musicSource.volume = info.Volume;
                _musicSource.Play();
                break;
            case Channel.SFX:
                _sfxSource.clip = info.Clip;
                _sfxSource.volume = info.Volume;
                _sfxSource.Play();
                break;
        }
    }

    private void PlayOneShot(PlayOneShotEvent e)
    {
        switch (e.channel)
        {
            case Channel.Music:
                _musicSource.volume = e.mySFXInfo.Volume;
                _musicSource.PlayOneShot(e.mySFXInfo.Clip);
                break;
            case Channel.SFX:
                _sfxSource.volume = e.mySFXInfo.Volume;
                _sfxSource.PlayOneShot(e.mySFXInfo.Clip);
                break;
        }
    }
}
