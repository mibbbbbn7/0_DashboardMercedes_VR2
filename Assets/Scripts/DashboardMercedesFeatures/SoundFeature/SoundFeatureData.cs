using System;
using Newtonsoft.Json;
using UnityEngine.Audio;

public class SoundFeatureData
{
    public const string AUDIO_MIXER_ASSET_PATH = "Audio/MyAudioMixer";
    public const string SOUND_SPEAKER_ASSET_NAME = "Audio/SoundSpeaker";
    public const string LOCAL_FILE_NAME = "local_sound_data.json";
    public const string SFX_GROUP_PATH = "Master/SFX";
    public const string MUSIC_GROUP_PATH = "Master/Music";

    public LocalSoundData MyLocalSoundData;
    public AudioMixer MyMixer;

    public SoundFeatureData()
    {
        MyLocalSoundData = new LocalSoundData
        {
            SFX = new SpecificChannel(),
            Music = new SpecificChannel()
        };
    }

}

[Serializable]
public class LocalSoundData
{
    [JsonProperty("sfx")] public SpecificChannel SFX;
    [JsonProperty("music")] public SpecificChannel Music;
}

[Serializable]
public class SpecificChannel
{
    [JsonProperty("mute")] public bool Mute = false;
    [JsonProperty("volume")] public float Volume = 1;
}
