using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene( "main_menu" );
        }
    }
}