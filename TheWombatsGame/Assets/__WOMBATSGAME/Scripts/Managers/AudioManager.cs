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

        musicTracks.MusicTrackAudioSource.Stop();
        
        if (SceneManager.GetActiveScene().name == "PlayerSelection")
        {
            UpdateMusicBtnIcon();
            UpdateSoundBtnIcon();
        }
       
    }

    [System.Serializable]
    public class MusicTracks
    {
       
        public AudioSource MusicTrackAudioSource;
        public AudioClip[] tracks;
        public string[] spotifyLinks;
        public bool mutedTrack;
        [SerializeField] public Image musicONIcon;
        [SerializeField] public Image musicOFFIcon;
    }

    [System.Serializable]
    public class SFX_All
    {
        public AudioSource countDownSound;
        public AudioSource switchLaneSound;
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
    public int i;
    public bool isTrackFinished,isTrackFinishedG;
   
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckSceneNameAndPlayTrack(SceneManager.GetActiveScene().name);
    }
    

    void CheckSceneNameAndPlayTrack(string sN)
    {
        if (sN == "LONDON")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[0];
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[1];
                    break;
            }
        }
        
        if (sN == "SYDNEY")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[2];
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[3];
                    break;
            }
        }
        
        if (sN == "ROME")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[4];
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[5];
                    break;
            }
        }
        
        if (sN == "HomeScreen" || sN == "PlayerSelection" || sN == "LevelSelection")
        {
            musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[6];
            
            if(!musicTracks.MusicTrackAudioSource.isPlaying) 
            {
                if(isMusicEnabled)
                    musicTracks.MusicTrackAudioSource.Play();
            }
        }
        
        if (sN == "Tutorial")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[3];
            
            if(isMusicEnabled)
                musicTracks.MusicTrackAudioSource.Play();
        }
    }

    public void LoadIcons()
    {
        sfxAll.SFXONIcon = GameObject.FindGameObjectWithTag("SFXON").GetComponent<Image>();
        sfxAll.SFXOFFIcon = GameObject.FindGameObjectWithTag("SFXOFF").GetComponent<Image>();
            
        musicTracks.musicONIcon = GameObject.FindGameObjectWithTag("MUSICON").GetComponent<Image>();
        musicTracks.musicOFFIcon = GameObject.FindGameObjectWithTag("MUSICOFF").GetComponent<Image>();
            
        UpdateMusicBtnIcon();
        UpdateSoundBtnIcon();
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
            
            isMusicEnabled = false;
        }

        else
        {
            musicTracks.mutedTrack = false;
            musicTracks.MusicTrackAudioSource.Play();
            
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
    
    private void Update()
    {
        
        // if (isMusicEnabled)
        // {
        //     if (musicTracks.MusicTrackAudioSource.gameObject.activeInHierarchy && !isTrackFinishedG)
        //     {
        //         isTrackFinishedG = true;
        //         Invoke("NextSong",musicTracks.MusicTrackAudioSource.clip.length);
        //     }
        // }
    }

    
}
