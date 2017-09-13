using UnityEngine;
using System.Collections;

public class BeginStoryScriptAlArrache : MonoBehaviour
{


	private void OnMouseUp()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene( "scene" );
	}
}