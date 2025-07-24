using UnityEngine;

public class PlayOneShotEvent
{
    public Channel channel;
    public readonly SFXInfoSO mySFXInfo;

    public PlayOneShotEvent(Channel channel, SFXInfoSO mySFXInfo)
    {
        this.mySFXInfo = mySFXInfo;
        this.channel = channel;
    }
}
