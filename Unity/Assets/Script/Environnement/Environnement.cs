using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public abstract class Environnement : MonoBehaviour
    {
        internal void SetEnvironnement (SoundManager.Sound sound)
        {
            SoundManager.GetSoundManager().SetAndPlayBackgroundMusic( sound );
        }
    }
}
