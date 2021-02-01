using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle FullScreenTog, Vsync;

    public ResItme[] Resolutions;

    public int SelectedResolution;

    public Text ResolutionLabel;

    public AudioMixer TheMixer;

    public Slider MasterSlider, MusicSlider, SFXSlider;
    public Text MasterLabel, MusicLable, SFXLabel;


    // Start is called before the first frame update
    void Start()
    {
        FullScreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            Vsync.isOn = false;
        }
        else
        {
            Vsync.isOn = true;
        }

        //search for resolution
        bool foundRes = false;
        for(int i = 0; i < Resolutions.Length; i++)
        {
            if(Screen.width == Resolutions[i].Horizontal && Screen.width == Resolutions[i].Vertical)
            {
                foundRes = true;

                SelectedResolution = i;

                UpdateResLabel();

            }
        }

        if(!foundRes)
        {
            ResolutionLabel.text = Screen.width.ToString() + " x " + Screen.height.ToString();
        }

        if(PlayerPrefs.HasKey("MasterVol"))
        {
            TheMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }

        if(PlayerPrefs.HasKey("MusicVol"))
        {
            TheMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }

        if(PlayerPrefs.HasKey("SFXVol"))
        {
            TheMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }

            MasterLabel.text = (MasterSlider.value + 80).ToString();
            MusicLable.text = (MusicSlider.value + 80).ToString();
            SFXLabel.text = (MusicSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        SelectedResolution--;
        if(SelectedResolution < 0)
        {
            SelectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        ResolutionLabel.text = Resolutions[SelectedResolution].Horizontal.ToString() + " x " + Resolutions[SelectedResolution].Vertical.ToString();
    }

    public void ResRight()
    {
        SelectedResolution++;
        if(SelectedResolution > Resolutions.Length -1)
        {
           SelectedResolution = Resolutions.Length -1;
        }
        UpdateResLabel();
    }


    public void ApplyGraphics()
    {
        //apply fullscreen
        //Screen.fullScreen = FullScreenTog.isOn;

        //Apply Vsync
        if(Vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        //sets resolution
        Screen.SetResolution(Resolutions[SelectedResolution].Horizontal,Resolutions[SelectedResolution].Vertical, FullScreenTog.isOn);

    }

    public void SetMasterVolumer()
    {
        MasterLabel.text = (MasterSlider.value + 80).ToString();

        TheMixer.SetFloat("MasterVol", MasterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", MasterSlider.value);

    }

    public void SetMusicVol()
    {
        MusicLable.text = (MusicSlider.value + 80).ToString();

        TheMixer.SetFloat("MusicVol", MusicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }

    public void SetSFXVol()
    {
        SFXLabel.text = (SFXSlider.value + 80).ToString();

        TheMixer.SetFloat("SFXVol", SFXSlider.value);

        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }
}

[System.Serializable]
public class ResItme {
    public int Horizontal, Vertical;
}
