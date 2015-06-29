using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class TempleForestEnvironnement : Environnement
    {
        void Start ()
        {
            SetEnvironnement( SoundManager.Sound.Music_ForestTemple );

            SoundManager.GetSoundManager().AudioSource.PlayOneShot( SoundManager.GetAudioClip (SoundManager.Sound.Music_Forest));
        }
    }
}