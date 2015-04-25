using System;
using UnityEngine;

//Todo : créer une classe spécifique pour tout les ennemis
public class AIController : MonoBehaviour
{
    #region Fields
    public float _randomMovementSpeed;
    public float _aggroMovementSpeed;
    public int _aggroArea;

	private Character _character;
	private static System.Random _randomGenerator = new System.Random();
    
    // Delay until the next move
	private int _changeDirection;        
	private int _randomDirection;

    // Time interval for the current move
	private int _timeOfMovement;
	private Vector3 _direction;

    // Time of the last Attack
	private float _lastAttack;
    #endregion

    void Start()
	{
        _direction = new Vector3();
		_lastAttack= Time.fixedTime;
		_character = GetComponent<Character>();
		_character.MovementSpeed = 2;
		_changeDirection = 0;
		_randomDirection = 1;
	}

	void Update()
	{
		if ( DetectTarget() )
		{
			AggressiveMove();
		}
		else
		{
			RandomMove();
		}
	}

    // TODO: Mettre le vecteur du mouvement en random entre -1 et 1 pour tout les axes sauf Y
	private void RandomMove()
	{     
		_character.MovementSpeed = _randomMovementSpeed;
		
		// Attribute a new direction to the character
		if ( _changeDirection == 0 )
		{
			_randomDirection = _randomGenerator.Next( 1, 4 );
			_changeDirection = _randomGenerator.Next( 100, 300 );
			_timeOfMovement = _randomGenerator.Next( 5, 100 );
		}
		
        // Set the direction
        _direction = Vector3.zero;
		switch ( _randomDirection )
		{
		    case 1:
			    _direction += Vector3.right;
			    break;
		    case 2:
			    _direction += Vector3.left;
			    break;
		    case 3:
			    _direction += Vector3.forward;
			    break;
		    case 4:
			    _direction += Vector3.back;
			    break;
		    default:
			    throw new Exception( "ERREUR _direction" );
		}

		_timeOfMovement--;

        //Move if direction is set
		if (_timeOfMovement > 0 )
		{
			_character.Move( _direction );
		}
		
		_changeDirection--;
		
	}

    private bool DetectTarget ()
    {
        GameObject player = GameObject.FindWithTag( "Player" );
        if ( player != null )
        {
            _direction = player.transform.position - _character.transform.position;
            if ( _direction.magnitude < _aggroArea )
            {
                return true;
            }
        }

        return false;
    }

    private void AggressiveMove()
	{
		_character.MovementSpeed = _aggroMovementSpeed;
		
        // Attack the enemy if he is near
		if ( _direction.magnitude < _character._attackRange )
		{
            if ( _lastAttack + _character.AttackSpeed < Time.fixedTime )
            {
				_lastAttack=Time.fixedTime;
				_character.Attack();	
			}
		}

		
		_character.Move( _direction );
		
	}

}
