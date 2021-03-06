﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
	public class ResumeGame : MonoBehaviour 
	{
		Button _button;
		GameObject _menu;

		void Awake()
		{
			gameObject.AddComponent<Image> ();
			_button = gameObject.AddComponent<Button> ();
			_button.image.overrideSprite = Resources.Load<Sprite>( "Images/PauseMenu/ButtonResumeGame" );
            _button.image.preserveAspect = true;
			_menu = GameObject.FindWithTag( "Menu" );
		}
		void Start () 
		{
			_button.onClick.AddListener( () => 
	        {
				_menu.SetActive(false) ;
				PauseManager.BlockEveryCharacter(false);
			});
		}

	}
}