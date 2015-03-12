using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	
	private CharacterController _controller;
	private float _speed;
	private float _speedRotation;
	private Vector3 _motion;
	
	// Use this for initialization
	void Start()
	{

		if ( _speed == 0 )
		{
			_speed = 5; 
			//Debug.Log( "Utilisation valeur par défaut" );
		}
		if ( _speedRotation == 0 )
		{
			_speedRotation = 10;
			//Debug.Log( "Utilisation valeur par défaut" );
		}
		
		_controller = this.GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update(){}
	
	// Testing _motion code

	public void Move(Vector3 direction) 
	{
		_controller.Move( direction * _speed * Time.deltaTime );
		_controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( direction ), _speedRotation * Time.deltaTime );
	}

	public void Attack() 
	{
		Debug.Log ("Attaque !");
		Vector3 cone_centre;
		cone_centre.z = 2;
		cone_centre.x = 0;
		cone_centre.y = 0;
		Debug.DrawRay(_controller.transform.position , _controller.transform.TransformDirection(cone_centre));
		
		Vector3 cone_droite = cone_centre;
		cone_droite.x += 1;
		Debug.DrawRay(_controller.transform.position , _controller.transform.TransformDirection(cone_droite));
		
		Vector3 cone_gauche = cone_centre;
		cone_gauche.x -= 1;
		Debug.DrawRay(_controller.transform.position , _controller.transform.TransformDirection(cone_gauche));



	}
	public void takeDamage() 
	{



	}


}