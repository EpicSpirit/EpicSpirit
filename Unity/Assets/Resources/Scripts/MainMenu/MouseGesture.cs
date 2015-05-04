using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class MouseGesture : MonoBehaviour
    {
        public void OnMouseUp()
        {
            if ( GetComponentInChildren<TextMesh>().text == "Play" )
            {
                GameObject.Find( "Spi" ).GetComponent<SpiController>().Fall( new Vector3( 0, 0, 10 ) );
                Invoke( "Play", 0.2f );
            }
            else if ( GetComponentInChildren<TextMesh>().text == "Exit" )
            {
                GameObject.Find( "Spi" ).GetComponent<SpiController>().Fall( new Vector3( 0, 0, -10 ) );
                Invoke( "Exit", 0.2f );
            }
        }

        private void Play()
        {
            Application.LoadLevel( "scene" );
        }
        private void Exit()
        {
            Debug.Log( "wtf!!!!" );
            Application.Quit();
        }
    }
}