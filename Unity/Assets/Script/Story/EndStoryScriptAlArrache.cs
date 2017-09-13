using UnityEngine;
using System.Collections;

public class EndStoryScriptAlArrache : MonoBehaviour {

	private void OnMouseUp()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene( "main_menu" );
	}
}
