using UnityEngine;
using System.Collections;

public class VeryBadBoy : Ennemi 
{
	
	private static System.Random _randomGenerator = new System.Random();
	
	
	// Use this for initialization
	void Start () 
	{
		Initialisation();
		if ( _aggroArea == 0 )
		{
			_aggroArea = 10;
		}
		_health = 30;
		_movementSpeed = 0.5f;
		
	}
	
	// Notre super boss ne peut que Invoker des BadBoys pour le moment
	public override void Attack ()
	{
		// Pour le moment notre méchant ne fait QUE Invoquer des BadBoy :-)
		InvokeBadBoy();
		
		
		
	}
	
	private void InvokeBadBoy() 
	{
		
		// On prend une position pas loin de notre boss
		Vector3 position = this.transform.position;
		
		position.x += _randomGenerator.Next(1,5);
		position.z += _randomGenerator.Next(1,5);

        GameObject obj = Instantiate( (UnityEngine.Object)UnityEngine.Resources.Load<UnityEngine.Object>( "BadBoy/Prefab_BadBoy" ), position, this.transform.rotation ) as GameObject;
		Ennemi ennemi = obj.GetComponent<Ennemi>();
		ennemi.Invokation();	
	}
	
	
	
	public override void takeDamage(int puissance) 
	{
		base.takeDamage(puissance);
	
	}
	
	
	
}
