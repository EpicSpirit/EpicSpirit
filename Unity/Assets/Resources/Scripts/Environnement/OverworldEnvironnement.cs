using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class OverworldEnvironnement : Environnement
    {
        void Start ()
        {
            SetEnvironnement( SoundManager.Sound.Music_Overworld );
        }
    }
}