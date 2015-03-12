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



	}
	public void takeDamage() 
	{

	}


}