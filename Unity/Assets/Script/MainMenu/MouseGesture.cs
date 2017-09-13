using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace EpicSpirit.Game
{
    public class MouseGesture : MonoBehaviour
    {
        public void OnMouseUp()
        {
            if ( GetComponentInChildren<TextMesh>().text == "Play" )
            {
                GameObject.Find( "Spi" ).GetComponent<SpiController>().Fall( new Vector3( 0, 0, 10 ) );
                Invoke( "Play", 0.3f );
            }
            else if ( GetComponentInChildren<TextMesh>().text == "Exit" )
            {
                GameObject.Find( "Spi" ).GetComponent<SpiController>().Fall( new Vector3( 0, 0, -10 ) );
                Invoke( "Exit", 0.2f );
            }
        }

        private void Play()
        {
            GameObject.FindWithTag( "Menu" ).transform.position = new Vector3( 40, 0, 0 );
            GameObject.FindWithTag( "Loading Screen" ).transform.position = new Vector3( 1.328523f, 0.8545985f, 2.893902f );

            SceneManager.LoadScene( "overworld" );
        }
        private void Exit()
        {
            Debug.Log( "wtf!!!!" );
            Application.Quit();
        }
    }
}