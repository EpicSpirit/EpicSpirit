﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class Character : MonoBehaviour
{
	// Controller du personnage
	internal CharacterController _controller;
	
	// Stat du perso
	internal float _speed;
	private float _speedRotation;
//		private Character _target; //A quoi ça sert ? 
	private int _attack;
	internal int _life;

	
	private Vector3 _motion;
	private RaycastHit _hit;
	internal int _lookaroundcount;
	internal Animation anims; // NON !
	
	public float _attackspeed;

    public float Speed
    {
        set { _speed = value; }
        get{ return _speed; }
    }
    
    

	// Use this for initialization
	virtual public void Start()
	{
		Initialisation();
		
		
	}
	
	internal void Initialisation() {
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
		
		
		
		
	
	}
	
	// Update is called once per frame
	virtual public void Update()
    {
		
        
	}
	internal void Gravity() {
		// Gravité // NON ! // Si ! C'est juste pour y aller Yolo pour le moment :p
        if ( !_controller.isGrounded )
        {
			_controller.Move( Vector3.down );
		}
	
	}
	
	
	public virtual void Move(Vector3 direction) 
	{	
		direction.y = 0;
        if ( !isAttacking() )
        {
            // Si on doit réellement bouger
            if ( direction != Vector3.zero )
            {
                // On joue l'anim walk 
                AnimationManager( "walk" );
                

                _controller.Move( direction * _speed * Time.deltaTime );
                _controller.transform.rotation = Quaternion.LookRotation( direction );
            }
            else
            {
            	AnimationManager( "idle" );
            }

        }
		
	}
	public bool isAttacking() {
		if(anims) {
            return anims.IsPlaying("bim") || anims.IsPlaying("bim_2");
		} else {
			return false;
		}
	}
	public virtual void Attack() 
	{	
		// Gestion du tick
		if(!isAttacking()) {
			
			List<Character> list_cible = GetListofCible(this.gameObject);
			foreach(Character adv in list_cible) {
				adv.takeDamage(1);
			}
			
			// Dans tout les cas on met l'anim d'attaque
			AnimationManager("bim");
		}

	}
	
	internal List<Character> GetListofCible(GameObject origin) {
		List<Character> retour = new List<Character>();
		List<Vector3> _vecteursAttaques;
		RaycastHit hit;
		Character adv = null;
		Vector3 new_origine = new Vector3(origin.transform.position.x, origin.transform.position.y+1, origin.transform.position.z);
		
		// Définition de la zone d'attaque
		_vecteursAttaques = new List<Vector3>();
		
		_vecteursAttaques.Add(new Vector3(0,0,2));
		_vecteursAttaques.Add(new Vector3(-1,0,2));	
		_vecteursAttaques.Add(new Vector3(1,0,2));
		
		foreach(Vector3 vec in _vecteursAttaques) {
		
			Debug.DrawRay(new_origine, origin.transform.TransformDirection(vec),Color.yellow,1.0f);
			
			if ( Physics.Raycast( new_origine, origin.transform.TransformDirection( vec ), out hit ,vec.magnitude) )
			{
				
				
				adv = hit.transform.GetComponent<Character>();
				
				if(adv != null && adv.name != this.name) 
				{			
					Debug.Log ("X :"+vec.x+"Y: "+vec.y+"Z"+vec.z+" name"+hit.transform.name);			
					//Debug.Log ("touché "+hit.collider.name);
					
					retour.Add(adv);
				}
			}
		}
		return retour;
	
	}

	public virtual void takeDamage(int puissance) 
	{
		// TODO a remettre quand l'effet de particule devient swaggy
        ParticleSystem[] par = this.GetComponentsInChildren<ParticleSystem>();
        foreach ( ParticleSystem par_ in par )
        {
            if ( par_.name == "DamageEffect" )
            {
				par_.Play();
			}
		}
		
		this._life -= puissance;

        if ( this._life <= 0 )
        {
            GameObject.Destroy( this.gameObject, 0.5f );
		}
        Debug.Log(this._life);
	}
	
	public void AnimationManager(string anim) {
		// Si notre gestionnaire d'anim existe bel et bien :
		if(anims != null &&  ! anims.IsPlaying(anim) ) {
		
			anims.Play(anim);
			
		}
		
		
	}
	
	
	public void ParticuleManager(string name) {
	
	   	var parts = this.GetComponentsInChildren<ParticleSystem>();
	   	foreach(ParticleSystem part in parts) {
	   		if(name == part.name) {
	   			part.Play();
	   		}
	   	
	   	}
	   	
	    
	}
	
}