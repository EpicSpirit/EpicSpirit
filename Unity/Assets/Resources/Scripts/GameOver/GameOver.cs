using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class GameOver : MonoBehaviour
    {
        void Awake () 
    {   
        SoundManager.GetSoundManager().Stop();
        Invoke( "BackMenu", 4f );
	}
        private void BackMenu ()
        {
            Application.LoadLevel( "main_menu" );
        }
    }
}