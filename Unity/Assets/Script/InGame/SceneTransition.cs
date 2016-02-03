using UnityEngine;
using System.Collections;


namespace EpicSpirit.Game
{
    public class SceneTransition : MonoBehaviour
    {

        public string scene;

        public void OnTriggerEnter ( Collider c )
        {
            if ( c.gameObject.tag == "Player" )
            {
                //Application.LoadLevel( scene );
                LevelManager.LoadLevel( scene );
            }
        }


    }
}