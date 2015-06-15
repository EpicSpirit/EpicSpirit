using UnityEngine;
using System.Collections;
using System;

namespace EpicSpirit.Game
{
    public class SoundManager : MonoBehaviour
    {
        static string _soundDirectory="Audio/";

        public enum Sound
        {
            Music_Forest,
            Effect_Fireball
        }

        static public void PlaySound ( AudioSource audioSource, Sound s )
        {
            PlaySound( audioSource, s, false );
        }

        static public void PlaySound ( AudioSource audioSource, Sound s, bool isLooping )
        {
            string path = Enum.GetName( typeof( Sound ), s );

            path = _soundDirectory +path.Replace( "_", "/" );

            audioSource.PlayOneShot( Resources.Load<AudioClip>( path ) );

        }

    }
}