using UnityEngine;
using System.Collections;

public class BadBoy : Ennemi 
{

	// Use this for initialization
	override public void Start () {
		base.Start();
		
		if ( _aggroArea == 0 )
		{
			_aggroArea = 8;
		}
		_health = 3;
		_movementSpeed = 2;
	}
	
	public override void takeDamage(int puissance) {
			
			base.takeDamage(puissance);
			if(!isAttacking()) {
			
				AnimationManager("damage");
			
			}
			
	
	}
	
	
	
}
