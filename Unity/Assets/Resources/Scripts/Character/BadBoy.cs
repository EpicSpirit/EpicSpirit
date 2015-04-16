using UnityEngine;
using System.Collections;

public class BadBoy : Ennemi 
{

	// Use this for initialization
	void Start () 
    {
		Initialisation();
		if ( _aggroArea == 0 )
		{
			_aggroArea = 8;
		}
		_life = 3;
		_movementSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
