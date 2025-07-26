using DashboardMercedes;
using UnityEngine;

public class Soundmanager : BaseSelfInjectedBehaviour<ISoundFeatureInternal, ISoundFeature>
{
    public void PlaySoundOneShot()
    {
        _featureBroadcaster.Broadcast(new PlayOneShotEvent(Channel.SFX));
    }
}
