using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
	public class ExitToMainMenu : MonoBehaviour 
	{
		Button _button;
		GameObject _menu;
		
		void Awake()
		{
			gameObject.AddComponent<Image> ();
			_button = gameObject.AddComponent<Button> ();
			_button.image.overrideSprite = Resources.Load<Sprite>( "Images/PauseMenu/ButtonExitToMainMenu" );
            _button.image.preserveAspect = true;
			_menu = GameObject.FindWithTag( "Menu" );
		}
		void Start () 
		{
			_button.onClick.AddListener( () => 
			                            {
				Debug.Log("ExitLevelToMainMenu");
				_menu.SetActive(false) ;
				Application.LoadLevel("main_menu");
			});
		}
		
	}
}