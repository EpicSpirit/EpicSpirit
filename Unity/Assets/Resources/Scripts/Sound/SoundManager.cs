using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;

namespace EpicSpirit.Game
{
    public class SoundManager : MonoBehaviour
    {
        static string _soundDirectory="Audio/";

        public enum Sound
        {
            Music_Forest,
            Music_ForestTemple,
            Music_GameMenu,
            Music_Overworld,
            Effect_Fireball
        }

        AudioSource _audioSource;
        AudioListener _audioListener;
        static SoundManager _soundManager;

        public AudioSource AudioSource
        {
            get { return _audioSource; }
        }

        void Awake()
        {
            _soundManager = this;
            DontDestroyOnLoad( this.gameObject );
            _audioSource = GetComponent<AudioSource>();
            _audioListener = GetComponent<AudioListener>();
        }

        public void SetAndPlayBackgroundMusic(AudioClip clip)
        {
            if ( _audioSource.clip != null ) _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();

            //_audioSource.PlayOneShot( clip );
        }

        public void SetAndPlayBackgroundMusic ( Sound sound )
        {
            SetAndPlayBackgroundMusic(GetAudioClip(sound));
        }

        static public AudioClip GetAudioClip(Sound s)
        {
            string path = Enum.GetName( typeof( Sound ), s );
            path = _soundDirectory + path.Replace( "_", "/" );
            return Resources.Load<AudioClip>( path );
        }

        public void PlaySound (Sound s)
        {
            _audioSource.PlayOneShot( GetAudioClip(s) );
        }
        
        public static SoundManager GetSoundManager()
        {
            if ( _soundManager == null )
            {
                return _soundManager = GameObject.Instantiate<GameObject>( Resources.Load<GameObject>( "Audio/SoundManager" ) ).GetComponent<SoundManager>();
            }
            else return _soundManager;
        }
    }
}