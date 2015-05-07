﻿using UnityEngine;
using System.Collections;

public class EndStory : MonoBehaviour
{
	float _defaultWidth;
	float _defaultHeight;
	Vector3 _scale;
	
	Transform _splashScreenTransform;
	
	void Start ()
	{
		_defaultWidth = 1366;
		_defaultHeight = 768;
		
		_splashScreenTransform = GetComponent<Transform>();
		
		_scale = new Vector3( Screen.width / _defaultWidth, Screen.height / _defaultHeight, 1 );
		
		_splashScreenTransform.localScale = _scale;
		
		Invoke( "LoadMainMenu", 10 );
	}
	
	private void LoadMainMenu()
	{
		Application.LoadLevel( "main_menu" );
	}
	private void OnMouseUp()
	{		
		Application.LoadLevel( "main_menu" );
	}
}
