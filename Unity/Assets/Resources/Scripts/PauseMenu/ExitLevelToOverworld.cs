using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
	public class ExitLevelToOverworld : MonoBehaviour 
	{
		Button _button;
		GameObject _menu;
		
		void Awake()
		{
			gameObject.AddComponent<Image> ();
			_button = gameObject.AddComponent<Button> ();
			_button.image.overrideSprite = Resources.Load<Sprite>( "Images/PauseMenu/ButtonExitLevelToOverworld" );
            _button.image.preserveAspect = true;
			_menu = GameObject.FindWithTag( "Menu" );
		}
		void Start () 
		{
			_button.onClick.AddListener( () => 
			{
				_menu.SetActive(false) ;
				Application.LoadLevel("overworld");
			});
		}
		
	}
}