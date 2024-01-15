using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioMixerGroup[] AudioMixerGroup = new AudioMixerGroup[2];
    [SerializeField] private Slider[] VolumeSlider = new Slider[2];
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Dropdown DropdownR;
    bool toggle = true;
    bool soundtoggle = false;
    Resolution[] resolutions;

    private void Awake()
    {
        float[] volumes = new float[2];

        AudioMixer.GetFloat("music", out volumes[0]);
        AudioMixer.GetFloat("voice", out volumes[1]);

        for (int i = 0; i < AudioMixerGroup.Length; i++)
        {
            VolumeSlider[i].value = volumes[i];
        }
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        DropdownR.ClearOptions();

        List<string> _options = new List<string>();
        int _currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string _option = resolutions[i].width + "x" + resolutions[i].height;
            _options.Add(_option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }

        DropdownR.AddOptions(_options);
        DropdownR.value = _currentResolutionIndex;
        DropdownR.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle = !toggle;
            ShowSettingsMenu(toggle);
        }
    }

    public void SetResolutions(int _resolutionIndex)
    {
        Resolution _resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("music", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        AudioMixer.SetFloat("voice", volume);
    }

    public void Fullscreen(bool _fullscreenSet)
    {
        Screen.fullScreen = _fullscreenSet;
    }

    public void ShowSettingsMenu(bool _show)
    {
        settingsMenu.SetActive(_show);
    }

    public void PlayExampleClip()
    {
        soundtoggle = !soundtoggle;

        if (soundtoggle)
        {
            audioSource.Play(); 
        } else
        {
            audioSource.Pause();
        }
    }
}