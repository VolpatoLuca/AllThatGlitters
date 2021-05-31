using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    //resolution, volume, fullscreen

    [SerializeField] AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        if (volume == -40f)
        {
            audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            audioMixer.SetFloat("Volume", volume);
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    [SerializeField] TMP_Dropdown resDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        GetResolutions();    
    }

    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>(); //mi serve per poterle scrivere nel dropdown
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height) //risoluzione attuale
            {
                currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
