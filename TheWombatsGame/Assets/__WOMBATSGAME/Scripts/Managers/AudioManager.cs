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
        public AudioSource carBlowingEngine;
        public AudioSource laneSwitch;
        public AudioSource[] crashSound;
        public AudioSource manScream;
        public AudioSource womanScream;
        public AudioSource coinCollect;
        public bool mutedTrack;
        [SerializeField] public Image SFXONIcon;
        [SerializeField] public Image SFXOFFIcon;
    }
    
    
    public MusicTracks musicTracks;
    [Space]
    public SFX_All sfxAll;
   
    
    
    public bool isHapticEnabled,isSFXenabled,isMusicEnabled;
    public int i;

    
    public Image HAPTICONIcon;
    public Image HAPTICOFFIcon;
    
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
        
        if (sN == "HomeScreen" || sN == "PlayerSelection" || sN == "LevelSelection")
        {
            musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[9];
            
            if(!musicTracks.MusicTrackAudioSource.isPlaying) 
            {
                if(isMusicEnabled)
                    musicTracks.MusicTrackAudioSource.Play();
            }
        }
        
        if (sN == "Tutorial")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[10];
            
            if(isMusicEnabled)
                musicTracks.MusicTrackAudioSource.Play();
        }
        
        if (sN == "LONDON")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[0];
                    i = 0;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[1];
                    i = 1;
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
                    i = 2;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[3];
                    i = 3;
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
                    i = 4;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[5];
                    i = 5;
                    break;
            }
        }
        
        if (sN == "LIVERPOOL")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[6];
                    i = 6;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[7];
                    i = 7;
                    break;
            }
        }
        
        if (sN == "PARIS")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[8];
                    i = 8;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[9];
                    i = 9;
                    break;
            }
        }
        
        if (sN == "EGYPT")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[10];
                    i = 10;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[11];
                    i = 11;
                    break;
            }
        }
        
        if (sN == "CARDIFF")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[0];
                    i = 0;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[1];
                    i = 1;
                    break;
            }
        }
        
        if (sN == "MILAN")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[2];
                    i = 2;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[3];
                    i = 3;
                    break;
            }
        }
        
        if (sN == "TOKYO")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[4];
                    i = 4;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[5];
                    i = 5;
                    break;
            }
        }
        
        if (sN == "GLASGOW")
        {
            musicTracks.MusicTrackAudioSource.Stop();
            
            switch (GameManager.Instance.lightingMode)
            {
                case 1:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[6];
                    i = 6;
                    break;
                case 2:
                    musicTracks.MusicTrackAudioSource.clip = musicTracks.tracks[7];
                    i = 7;
                    break;
            }
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
            
            musicTracks.musicONIcon.gameObject.SetActive(true);
            musicTracks.musicOFFIcon.gameObject.SetActive(false);
            
            isMusicEnabled = false;
        }

        else
        {
            musicTracks.mutedTrack = false;
            musicTracks.MusicTrackAudioSource.Play();
            
            musicTracks.musicONIcon.gameObject.SetActive(false);
            musicTracks.musicOFFIcon.gameObject.SetActive(true);
            
            isMusicEnabled = true;
        }
        
    }
    
    
    public void ToggleSFX()
    {
        if (!sfxAll.mutedTrack)
        {
            sfxAll.mutedTrack = true;
            sfxAll.countDownSound.mute = true;

            /*sfxAll.SFXONIcon.gameObject.SetActive(true);
            sfxAll.SFXOFFIcon.gameObject.SetActive(false);*/
            
            isSFXenabled = false;

        }

        else
        {
            sfxAll.mutedTrack = false;
            sfxAll.countDownSound.mute = false;
            
            /*sfxAll.SFXONIcon.gameObject.SetActive(false);
            sfxAll.SFXOFFIcon.gameObject.SetActive(true);*/

            isSFXenabled = true;
        }
        
    }
    
    public void ToggleHaptic()
    {
        if (isHapticEnabled)
        {
            isHapticEnabled = false;
            /*HAPTICONIcon.gameObject.SetActive(true);
            HAPTICOFFIcon.gameObject.SetActive(false);*/
        }

        else if((!isHapticEnabled))
        {
            isHapticEnabled = true;
            /*HAPTICONIcon.gameObject.SetActive(false);
            HAPTICOFFIcon.gameObject.SetActive(true);*/
        }
        
    }

    public void HapticOn()
    {
        isHapticEnabled = true;
    }

    public void HapticOff()
    {
        isHapticEnabled = false;
    }
    
    
    
   
}
