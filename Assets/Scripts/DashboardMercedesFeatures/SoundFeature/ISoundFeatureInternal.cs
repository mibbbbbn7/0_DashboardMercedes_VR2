using DashboardMercedes;
using UnityEngine;

public interface ISoundFeatureInternal : IFeatureInternal
{
    public LocalSoundData GetLocalData { get; }

    void SaveSettings(float value1, float value2);
}
