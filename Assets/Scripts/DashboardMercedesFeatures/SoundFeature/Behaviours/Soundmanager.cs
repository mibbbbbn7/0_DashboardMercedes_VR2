using DashboardMercedes;
using UnityEngine;

public class Soundmanager : BaseSelfInjectedBehaviour<ISoundFeatureInternal, ISoundFeature>
{
    [SerializeField] private SFXInfoSO _mySFXInfo;

    public void PlaySoundOneShot()
    {
        _featureBroadcaster.Broadcast(new PlayOneShotEvent(Channel.SFX, _mySFXInfo));
    }
}
