using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class GameMenuEnvironnement : Environnement
    {
        void Start ()
        {
            SetEnvironnement( SoundManager.Sound.Music_GameMenu );
        }
    }
}