using UnityEngine;
using System.Collections;

public class BadBoy : Character 
{
	public override void Start () 
    {
        base.Start();

		_health = 3;
		_movementSpeed = 2;

	}

}
