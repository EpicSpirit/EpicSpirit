using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{
	
	private CharacterController _controller;

    #region Char Stats

    private float _speed;
	private float _speedRotation;
	private Character _target;
	private int _health;
    private int _attack;

    #endregion

	private RaycastHit _hit;
    
    private int _lookAroundCount;

    #region Properties
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public float Speed
    {
        set { _speed = value; }
    }
    public int LookAroundCount
    {
        set { _lookAroundCount = value; }
    }
    #endregion

    // Use this for initialization
	void Start()
	{
		
		
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
	void Update()
    {
		// Gravité
        this.GetComponent<CharacterController>().Move( Vector3.down );
	}
	
	// Testing _motion code
	
	public void Move(Vector3 direction) 
	{
        if ( _controller != null )
        { 
		// Si on doit réellement bouger
            if ( direction != Vector3.zero )
            {
                // On joue l'anim walk si elle existe
                Animation list = _controller.gameObject.GetComponentInChildren<Animation>();
                if ( list )
                {
                    list.CrossFade( "walk" );
                }

                _controller.Move( direction * _speed * Time.deltaTime );
                _controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( direction ), _speedRotation * Time.deltaTime );
            }
            else
            {
                // Joue l'anime idle
                Animation anim = _controller.gameObject.GetComponentInChildren<Animation>();
                if ( anim )
                {
                    anim.CrossFadeQueued( "idle" );
                }
                // Gestion du mouvement lookaround quand on reste static un petit moment
                _lookAroundCount--;
                if ( _lookAroundCount < 0 )
                {
                    _lookAroundCount = 300;
                    anim.CrossFadeQueued( "look_around", 1f, QueueMode.PlayNow );
                }
            }
		}
	}

	public void Attack() 
	{
        if ( _controller != null )
        {
            Debug.Log( "Attaque !" );
            Vector3 cone_centre;
            cone_centre.z = 2;
            cone_centre.x = 0;
            cone_centre.y = 0;

            Vector3 cone_droite = cone_centre;
            Vector3 cone_gauche = cone_centre;
            cone_droite.x += 1;
            cone_gauche.x -= 1;

            if ( Physics.Raycast( _controller.transform.position, _controller.transform.TransformDirection( cone_centre ), out _hit ) )
            {
                _target = null;
                _target = _hit.transform.GetComponent<Character>();
                if ( _target != null )
                {
                    _target.takeDamage();
                }
            }

            Animation anim = _controller.gameObject.GetComponentInChildren<Animation>();
            if ( anim )
            {
                //anim.CrossFadeQueued("bim",1f, QueueMode.PlayNow);
                anim.Play( "bim" );
            }
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
		this._health -= 1;

        Debug.Log( "Je suis " + this.name + " et je viens de prendre des dégats :-(" );
        Debug.Log( "Vie restante : " + this._health );

        if ( this._health <= 0 )
        {
            Die();
		}
	}

    private void Die()
    {
        GameObject.Destroy( this.gameObject, 0.5f );
    }
}