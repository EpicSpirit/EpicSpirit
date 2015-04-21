﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public  class Character : MonoBehaviour
{
    #region Fields

	public float _movementSpeed;
    public float _attackRange;

    // Character Statistic
    internal float _speedRotation;
    internal int _attack;
	internal int _health;
    internal float _attackSpeed = 5f;

    // Utilities
    internal RaycastHit _hit;
	internal int _lookAroundCount;
    internal Animation _animations;
    internal List<Character> _targets;
    internal List<Vector3> _attackVectors;

    // Controller
    private CharacterController _characterController;

    #endregion 

    #region Properties
    public float MovementSpeed
    {
        set { _movementSpeed = value; }
        get { return _movementSpeed; }
    }
    public float AttackSpeed
    {
        set { _attackSpeed = value; }
        get { return _attackSpeed; }
    }

    #endregion

	public virtual void Start()
	{
        InitializeAnimationManager();

        _lookAroundCount = 300;
        _health = 3;

        _attackVectors = new List<Vector3>();
        _attackVectors.Add( new Vector3( 0, 1, 2 ) );
        _attackVectors.Add( new Vector3( -1, 1, 2 ) );
        _attackVectors.Add( new Vector3( 1, 1, 2 ) );

        _characterController = this.GetComponent<CharacterController>();
        if ( _characterController == null ) 
        { 
            throw new NullReferenceException("Character must have a CharacterController"); 
        }

		
	}

    public virtual void Update ()
    {

    }

    private void InitializeAnimationManager()
    {
        _animations = this.GetComponent<Animation>();
        if (_animations == null)
        {
            _animations = this.GetComponentInChildren<Animation>();
        }

        if (_animations == null)
        {
            throw new NullReferenceException("Character must have Animation Component"); 

        }
    }
	
	public void Move(Vector3 direction) 
	{	
		direction.y = 0;
        if ( !isAttacking() )
        {
            // Character have to move
            if ( direction != Vector3.zero )
            {
                AnimationManager( "walk" );
                _characterController.Move( direction * _movementSpeed * Time.deltaTime );
                _characterController.transform.rotation = Quaternion.LookRotation( direction );
            }
            else
            {
                if ( _animations && !_animations.IsPlaying( "look_around" ) )
                {
                    AnimationManager( "idle" );
                }

                // LookAround Management
                _lookAroundCount--;
                if ( _lookAroundCount < 0 )
                {
                    _lookAroundCount = 300;
                    AnimationManager( "look_around" );
                }
            }

        }
		
	}
	internal bool isAttacking() 
    {
		if( _animations )
        {
            return _animations.IsPlaying( "bim" ) || _animations.IsPlaying( "bim_2" );
		}
        else 
        {
			return false;
		}
	}
	
    // Todo: Mettre en place les vecteur d'attaque en fonction du attack_range
    public virtual void Attack() 
	{	
		// Tick Management
		if( ! isAttacking() ) 
        {
            GetListOfTarget( );
            foreach ( Character enemy in _targets ) 
            {
                enemy.takeDamage( 1 );    // TODO: Gérer la force d'attaque
			}

            AnimationManager( "bim" );
		}

	}
	
    // TODO : remettre l'origine de l'attaque au bon endroit
	internal List<Character> GetListOfTarget() 
    {
        _targets = new List<Character>();
        Vector3 realAttackOrigin = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);

        foreach ( Vector3 vector in _attackVectors )
        {
            Debug.DrawRay( realAttackOrigin, this.transform.TransformDirection( vector ), Color.yellow, 1.0f );

            if ( Physics.Raycast( realAttackOrigin, this.transform.TransformDirection( vector ), out _hit ) )
			{

				Character target = null;
                target = _hit.transform.GetComponent<Character>();

                if ( target != null && target.name != this.name ) 
				{
                    _targets.Add( target );
				}
			}
		}
        return _targets;
	
	}

	// TODO: Rendre ça plus propre au niveau algo
    // TODO : Faire la migration dans le particule manager
    internal virtual void takeDamage(int force) 
	{
        ParticleSystem[] particuleSystems = this.GetComponentsInChildren<ParticleSystem>();
        foreach ( ParticleSystem particuleSystem in particuleSystems )
        {
            if ( particuleSystem.name == "DamageEffect" )
            {
                particuleSystem.Play();
			}
		}

		_health -= force;

        if ( _health <= 0 )
        {
            GameObject.Destroy( this.gameObject, 0.5f );
		}

	}
	
	public void AnimationManager(string anim) {
		if( _animations != null &&  ! _animations.IsPlaying(anim) && _animations.GetClip(anim) != null ) 
        {
			_animations.Play( anim );	
		}
	}
	
	// TODO : Rendre ça opti
    public void ParticuleManager(string name) 
    {
	   	ParticleSystem[] particulesSystem = this.GetComponentsInChildren<ParticleSystem>();

	   	foreach(ParticleSystem particuleSystem in particulesSystem) 
        {
	   		if(name == particuleSystem.name) 
            {
                particuleSystem.Play();
            }
	   	
	   	}
	   	
	    
	}
	

    // Gestion des etats

    // Est ce que l'on est dans tel etat

    enum State
    {
        Idle=1,
        Walk=2,
        Attack=4,
        Damaged=8,
        Dead=16,
        Cinematic=32
    }
    State _state;
    List<int> Mask;

    public void InitializeStateManager () 
    {
        Mask [0] = 32; // Idle
        Mask [1] = 48; // Walk
        Mask [2] = 48; // Attack
        Mask [3] = 56; // Damaged
        Mask [4] = 62; // Dead
        Mask [5] = 63; // Cinematic
    }

    /// <summary>
    /// Return true if the state is right
    /// </summary>
    /// <param name="state">The state to match</param>
    /// <returns>True if right</returns>
    public bool isState(State state ) 
    {
        if ( state == null ) { throw new ArgumentNullException(); }

        return _state == state;
    }

    /// <summary>
    /// Change State if it's possible
    /// </summary>
    /// <param name="state">The new state</param>
    /// <returns>True if the state has changed</returns>
    public bool ChangeState (State state) 
    {
        if ( state == null ) { throw new ArgumentNullException(); }

        if ( isPriority( _state, state ) ) 
        {
            _state = state; return true; 
        }
        else 
        {
            return false; 
        }
    }

    /// <summary>
    /// Compare the priority of both states
    /// </summary>
    /// <param name="currentState"></param>
    /// <param name="newState"></param>
    /// <returns>True if the new state is priority than the current state</returns>
    private bool isPriority (State currentState, State newState) 
    {

        throw new NotImplementedException();

     }

    private int powerTwo ( int value ) { 
        var a = 0;
        do {
            value/=2;
            a++;
        } while(value!= 1);
        return a;
    }

}