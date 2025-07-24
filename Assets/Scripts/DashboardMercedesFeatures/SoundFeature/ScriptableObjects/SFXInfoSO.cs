using UnityEngine;

[CreateAssetMenu(fileName = "MySFXInfo", menuName = "ScriptableObjects/New SFX Info")]
public class SFXInfoSO : ScriptableObject
{
    public AudioClip Clip;
    public float Volume;
    [SerializeField] private float Pitch;

    public virtual float GetPitch => Pitch;
}

[CreateAssetMenu(fileName = "MyRandomSFX", menuName = "ScriptableObjects/New Random SFX Info")]
public class RandomSFXInfoSO : SFXInfoSO
{
    [SerializeField] private float MinPitch;
    [SerializeField] private float MaxPitch;

    public override float GetPitch => Random.Range(MinPitch, MaxPitch);
}
