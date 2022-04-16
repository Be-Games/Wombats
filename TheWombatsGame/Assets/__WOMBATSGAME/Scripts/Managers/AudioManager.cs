using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        isHapticEnabled = true;
        isMusicEnabled = true;
        isSFXenabled = true;

        if (SceneManager.GetActiveScene().name == "HomeScreen")
        {
            UpdateMusicBtnIcon();
            UpdateSoundBtnIcon();
        }
       
    }

    [System.Serializable]
    public class MusicTracks
    {
        public AudioSource mainMenuAudioSource;
        public AudioSource MusicTrackAudioSource;
        public AudioClip[] tracks;
        public bool mutedTrack;
        [SerializeField] public Image musicONIcon;
        [SerializeField] public Image musicOFFIcon;
    }

    [System.Serializable]
    public class SFX_All
    {
        public AudioSource countDownSound;
        public AudioSource brakeSound;
        public bool mutedTrack;
        [SerializeField] public Image SFXONIcon;
        [SerializeField] public Image SFXOFFIcon;
    }
    
    
    [System.Serializable]
    public class HapticFeedbacks
    {
        
    }

    public MusicTracks musicTracks;
    [Space]
    public SFX_All sfxAll;
   
    
    
    public bool isHapticEnabled,isSFXenabled,isMusicEnabled;

    private void Start()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            musicTracks.mainMenuAudioSource.gameObject.SetActive(true);
            musicTracks.MusicTrackAudioSource.gameObject.SetActive(false);
        }
    }

    public void Play(AudioSource audioSource)
    {
        if (isSFXenabled)
        {
            audioSource.Play();
        }

        if (isMusicEnabled)
        {
            audioSource.Play();
        }
    }

    public void ToggleMusic()
    {
        if (!musicTracks.mutedTrack)
        {
            musicTracks.mutedTrack = true;
            musicTracks.MusicTrackAudioSource.Pause();
            
            if(musicTracks.mainMenuAudioSource.gameObject.activeInHierarchy)
                musicTracks.mainMenuAudioSource.Pause();

            isMusicEnabled = false;
        }

        else
        {
            musicTracks.mutedTrack = false;
            musicTracks.MusicTrackAudioSource.Play();
            
            if(musicTracks.mainMenuAudioSource.gameObject.activeInHierarchy)
                musicTracks.mainMenuAudioSource.Play();

            isMusicEnabled = true;
        }
        
        UpdateMusicBtnIcon();
    }

    public void UpdateMusicBtnIcon()
    {
        if (!musicTracks.mutedTrack)
        {
            musicTracks.musicONIcon.gameObject.SetActive(true);
            musicTracks.musicOFFIcon.gameObject.SetActive(false);
        }

        else
        {
            musicTracks.musicONIcon.gameObject.SetActive(false);
            musicTracks.musicOFFIcon.gameObject.SetActive(true);
        }
    }
    
    public void ToggleSFX()
    {
        if (!sfxAll.mutedTrack)
        {
            sfxAll.mutedTrack = true;
            sfxAll.countDownSound.mute = true;

            isSFXenabled = false;

        }

        else
        {
            sfxAll.mutedTrack = false;
            sfxAll.countDownSound.mute = false;

            isSFXenabled = true;
        }
        
        UpdateSoundBtnIcon();
    }

    public void UpdateSoundBtnIcon()
    {
        if (!sfxAll.mutedTrack)
        {
            sfxAll.SFXONIcon.gameObject.SetActive(true);
            sfxAll.SFXOFFIcon.gameObject.SetActive(false);
        }

        else
        {
            sfxAll.SFXONIcon.gameObject.SetActive(false);
            sfxAll.SFXOFFIcon.gameObject.SetActive(true);
        }
    }
}
