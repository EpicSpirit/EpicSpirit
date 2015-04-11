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
		_speed = 0.5f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
			
					
	}
	
	// Notre super boss ne peut que Invoker des BadBoys pour le moment
	public override void Attack ()
	{
		// Pour le moment notre méchant ne fait QUE Invoquer des BadBoy :-)
		InvokeBadBoy();
		
		
		
	}
	
	private void InvokeBadBoy() {
		
		// On prend une position pas loin de notre boss
		Vector3 position = this.transform.position;
		
		position.x += _randomGenerator.Next(1,5);
		position.z += _randomGenerator.Next(1,5);
		
		var obj = Instantiate(Resources.LoadAssetAtPath<UnityEngine.Object>("Assets/BadBoy/Ennemi 1.prefab"),position, this.transform.rotation) as GameObject;
		var ennemi = obj.GetComponent<Ennemi>();
		ennemi.Invokation();	
	}
	
	
	
	public override void takeDamage(int puissance) {
		Debug.Log ("AIE ! "+ _life);
		base.takeDamage(puissance);
	
	}
	
	
	
}
