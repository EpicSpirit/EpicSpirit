using UnityEngine;
using System.Collections;

public class Boss_OTD : Ennemi {
	
	private static System.Random _randomGenerator = new System.Random();
	
	
	// Use this for initialization
	void Start () {
		Initialisation();
		if ( _aggroArea == 0 )
		{
			_aggroArea = 10;
		}
		_life = 30;
		_speed = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
		
			
					
	}
	
	// Notre super boss ne peut que Invoker des BadBoys pour le moment
	public override void Attack ()
	{
		// On prend une position pas loin de notre boss
		Vector3 position = this.transform.position;
		
		position.x += _randomGenerator.Next(1,5);
		position.z += _randomGenerator.Next(1,5);
		
		Instantiate(Resources.Load ("BadBoy/Ennemi 1"),position, this.transform.rotation);
		Debug.Log ("Instantiate badboy");
		
		
	}
	
	
	
	
}
