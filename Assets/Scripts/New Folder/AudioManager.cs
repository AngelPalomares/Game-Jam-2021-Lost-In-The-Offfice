using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] SFX, Music;
    public AudioSource Main_Theme, ElevatorMusic;
    public AudioMixer TheMixer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            TheMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));

        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            TheMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));

        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            TheMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int SoundToPlay)
    {
        SFX[SoundToPlay].Stop();
        SFX[SoundToPlay].Play();
    }

    public void PlayMusic(int SoundToPlay)
    {
        Music[SoundToPlay].Stop();
        Music[SoundToPlay].Play();
    }

    public void MainThemePlay()
    {
        ElevatorMusic.Stop();
        Main_Theme.Play();
    }

    public void ElevatorrMusic()
    {
        Main_Theme.Stop();
        ElevatorMusic.Play();
    }
}
