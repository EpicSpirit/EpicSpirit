using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	void Start () 
    {
        Invoke( "BackMenu", 4f );
	}
	private void BackMenu()
    {
        Application.LoadLevel( "main_menu" );
    }
}
