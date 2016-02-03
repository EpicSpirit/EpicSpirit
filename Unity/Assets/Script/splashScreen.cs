using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var animation = this.GetComponent<Animation>();
        animation.Play();
        Invoke( "LoadMenu", 5f );
	}
	
	void LoadMenu()
    {
        Application.LoadLevel( "main_menu" );
    }
}
