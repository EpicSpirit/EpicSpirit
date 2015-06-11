using UnityEngine;
using System.Collections;
using System;

namespace EpicSpirit.Game
{
    public class SoundManager : MonoBehaviour
    {
        static string _soundDirectory="./Audio";

        public enum Sound
        {
            Music_Forest,
            Effect_Fireball
        }

        static void PlaySound ( AudioSource audioSource, Sound s )
        {
            PlaySound( audioSource, s, false );

        }

        static void PlaySound ( AudioSource audioSource, Sound s, bool isLooping )
        {
            string path = Enum.GetName( typeof( Sound ), s );
            path = path.Replace( "_", "/" );
            audioSource.PlayOneShot( Resources.Load<AudioClip>( _soundDirectory + path ) );

        }

    }
}