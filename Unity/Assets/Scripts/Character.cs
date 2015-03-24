using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	private List<Vector3> _vecteursAttaques;
	private Animation anims;
	
	private float _lastAttack;
	public float _attackspeed;

    public float Speed
    {
        set { _speed = value; }
        get{ return _speed; }
    }
    
    

	// Use this for initialization
	void Start()
	{
		_lastAttack=0;
		// Gestion des mauvaises init
		if(_attackspeed == 0) {_attackspeed = 0.5f;}
		
		// Animation Manager
		anims = this.GetComponent<Animation>();
		if(anims == null) {
			anims = this.GetComponentInChildren<Animation>();
		}
	
		_lookaroundcount = 300;
		_life =3;
        _hit = new RaycastHit();

		if ( _speed == 0 )
		{
			_speed = 100; 
			//Debug.Log( "Utilisation valeur par défaut" );
		}
		if ( _speedRotation == 0 )
		{
			_speedRotation = 10;
			//Debug.Log( "Utilisation valeur par défaut" );
		}
		_controller = this.GetComponent<CharacterController>();
		
		// Définition de la zone d'attaque
		_vecteursAttaques = new List<Vector3>();
		_vecteursAttaques.Add(new Vector3(0,0,2));
		_vecteursAttaques.Add(new Vector3(1,0,2));
		_vecteursAttaques.Add(new Vector3(-1,0,2));
		
		_vecteursAttaques.Add(new Vector3(0,1,2));
		_vecteursAttaques.Add(new Vector3(-1,1,2));
		_vecteursAttaques.Add(new Vector3(1,1,2));
		
		
		
		
	}
	
	// Update is called once per frame
	void Update()
    {
		// Gravité
        this.GetComponent<CharacterController>().Move( Vector3.down );
        
	}
	
	
	
	public void Move(Vector3 direction) 
	{
		if(!isAttacking()) {
			// Si on doit réellement bouger
			if( direction != Vector3.zero )
			{
				// On joue l'anim walk 
				AnimationManager("walk");
				
				
				_controller.Move( direction * _speed * Time.deltaTime );
				_controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( direction ), _speedRotation * Time.deltaTime );
			}
			else
			{
				AnimationManager("idle");
				
				// Gestion du mouvement lookaround quand on reste static un petit moment
				_lookaroundcount --;
				if(_lookaroundcount < 0)
				{
					_lookaroundcount = 300;
					AnimationManager("look_around");
				}	
			}
		
		}
		
	}
	public bool isAttacking() {
		if(anims) {
			return anims.IsPlaying("bim");
		} else {
			return false;
		}
	}
	public void Attack() 
	{	
		// Gestion du tick
		if(!isAttacking()) {
			_lastAttack = Time.fixedTime;	// MaJ
			
			// Qui on attaque
			foreach(Vector3 vec in _vecteursAttaques) {
				Debug.DrawRay(_controller.transform.position, _controller.transform.TransformDirection(vec),Color.yellow,1.0f);
				
				if ( Physics.Raycast( _controller.transform.position, _controller.transform.TransformDirection( vec ), out _hit ) )
				{
					//DEBUG 
					_adv= null;
					_adv = _hit.transform.GetComponent<Character>();
					
					if(_adv != null) 
					{						
						_adv.takeDamage();
					}
				}
			}
			
			// Dans tout les cas on met l'anim d'attaque
			AnimationManager("bim");
			
			
		}
		
		
		

	}

	public void takeDamage() 
	{
        ParticleSystem[] par = this.GetComponentsInChildren<ParticleSystem>();
        foreach ( ParticleSystem par_ in par )
        {
            if ( par_.name == "DamageEffect" )
            {
				par_.Play();
			}
		}
		this._life -= 1;

        if ( this._life <= 0 )
        {
            GameObject.Destroy( this.gameObject, 0.5f );
		}
	}
	
	private void AnimationManager(string anim) {
		// Si notre gestionnaire d'anim existe bel et bien :
		if(anims != null) {
		
			anims.CrossFade(anim);
			
		}
	}
	
	
	public void ParticuleManager() {
	
	
	
	}
	
}