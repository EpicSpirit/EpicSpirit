using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	void Awake () 
    {
        Invoke( "BackMenu", 4f );
	}
	private void BackMenu()
    {
        Application.LoadLevel( "main_menu" );
    }
}
