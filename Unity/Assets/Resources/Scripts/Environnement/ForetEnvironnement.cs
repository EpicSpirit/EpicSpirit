using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ForetEnvironnement : Environnement
    {
        void Start ()
        {
            SetEnvironnement(SoundManager.Sound.Music_Forest);
        }
    }
}