using System;
using DashboardMercedes;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : BaseSelfInjectedBehaviour<ISoundFeatureInternal, ISoundFeature>
{
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Button _backToMenuButton;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        _backToMenuButton.onClick.AddListener(SaveSettings);

        _sfxSlider.value = _feature.GetLocalData.SFX.Volume;
        _musicSlider.value = _feature.GetLocalData.Music.Volume;
    }

    private void SaveSettings()
    {
        _feature.SaveSettings(_musicSlider.value, _sfxSlider.value);
    }
}
