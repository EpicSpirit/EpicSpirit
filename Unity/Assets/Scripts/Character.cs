using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	
	private CharacterController _controller;
	private float _speed;
	private float _speedRotation;
	private Vector3 _motion;
	private RaycastHit _hit;
	private Character _adv;
	private int _life;
	private int _lookaroundcount;
	
	// Use this for initialization
	void Start()
	{
		_lookaroundcount = 300;
		_life =3;
		_hit=new RaycastHit();

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
	void Update(){
		// Gravité
		this.GetComponent<CharacterController>().Move(Vector3.down);
	}
	
	// Testing _motion code
	public void DontMove() {
		Animation list = _controller.gameObject.GetComponentInChildren<Animation>();
		if(list) {
			list.CrossFade("idle");
		}	
	}
	public void Move(Vector3 direction) 
	{
		
		// Si on doit réellement bouger :
		if(direction != Vector3.zero) {
			// On joue l'anim walk si elle existe
			Animation list = _controller.gameObject.GetComponentInChildren<Animation>();
			if(list) {
				list.CrossFade("walk");
			}
		
			_controller.Move( direction * _speed * Time.deltaTime );
			_controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( direction ), _speedRotation * Time.deltaTime );
		}
		else {
			// Joue l'anime idle
			Animation anim = _controller.gameObject.GetComponentInChildren<Animation>();
			if(anim) {
				anim.CrossFadeQueued("idle");
			}	
			// Gestion du mouvement lookaround quand on reste static un petit moment
			_lookaroundcount --;
			if(_lookaroundcount < 0) {
				_lookaroundcount = 300;
				anim.CrossFadeQueued("look_around",1f, QueueMode.PlayNow);
			
			}
			
		
		}
	}

	public void Attack() 
	{
		Debug.Log ("Attaque !");
		Vector3 cone_centre;
		cone_centre.z = 2;
		cone_centre.x = 0;
		cone_centre.y = 0;

		Vector3 cone_droite = cone_centre;
		Vector3 cone_gauche = cone_centre;
		cone_droite.x += 1;
		cone_gauche.x -= 1;

		if(Physics.Raycast(_controller.transform.position, _controller.transform.TransformDirection(cone_centre), out _hit)) {
			_adv=null;
			_adv = _hit.transform.GetComponent<Character>();
			if(_adv != null) {
				_adv.takeDamage();

			}
		}


	}

	public void takeDamage() 
	{
		ParticleSystem[] par = this.GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem par_ in par) {
			if(par_.name == "DamageEffect") {
				par_.Play();
			}

		}
		this._life -= 1;

		Debug.Log ("Je suis "+this.name+" et je viens de prendre des dégats :-(");
		Debug.Log ("Vie restante : "+this._life);

		if(this._life <= 0) {

			GameObject.Destroy(this.gameObject, 0.5f);

		}

	}


}