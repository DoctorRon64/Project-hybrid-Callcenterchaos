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
    [SerializeField] private AudioSource audioSourceVoice;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Dropdown DropdownR;
    bool toggle = true;
    bool soundtoggle1 = false;
    bool soundtoggle2 = false;
    Resolution[] resolutions;

    private void Awake()
    {
        settingsMenu.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Tab))
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

    public void PlayVoiceClip()
    {
        soundtoggle1 = !soundtoggle1;

        if (soundtoggle1)
        {
            audioSourceVoice.Play(); 
        } else
        {
            audioSourceVoice.Pause();
        }
    }

    public void PlayMusicClip()
    {
        soundtoggle2 = !soundtoggle2;

        if (soundtoggle2)
        {
            audioSourceMusic.Play();
        }
        else
        {
            audioSourceMusic.Pause();
        }
    }
}