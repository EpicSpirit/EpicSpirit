using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{

    public string scene;

    public void OnTriggerEnter ( Collider c )
    {
        if ( c.gameObject.name == "Spi" )
        {
            Application.LoadLevel( scene );
        }
    }


}