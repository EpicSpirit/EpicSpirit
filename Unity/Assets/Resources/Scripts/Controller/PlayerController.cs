﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IController
{
    public Character _character;

    private bool _attack;

	// Use this for initialization
	void Start () 
    {
        _attack = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ( _character != null )
        {
    	
			Vector3 direction = new Vector3();
			// Test pour la gestion des touches pour le moment
			if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) )
			{
				direction += Vector3.right;
			}
			if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.Q ) )
			{
				direction += Vector3.left;
				
			}
			if ( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.Z ) )
			{
				direction += Vector3.forward;
				
			}
			if ( Input.GetKey( KeyCode.DownArrow ) || Input.GetKey( KeyCode.S ) )
			{
				direction += Vector3.back;
			}
			
			// Application de la méthode Move si on doit bouger
			_character.Move( direction );
			
			if ( Input.GetKeyDown( KeyCode.Space ) || _attack == true ) 
			{
                _attack = false;
				_character.Attack();
			}
    	}
	}

    public void ToAttack()
    {
        _attack = true;
    }
	
	public Vector3 Direction()
    {
        return new Vector3();
    }

}
