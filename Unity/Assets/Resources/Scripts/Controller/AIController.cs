using UnityEngine;

public class AIController : MonoBehaviour, IController
{
	private Character _character;
	
	private static System.Random _randomGenerator = new System.Random();
	
	private int _changeDirection;
	private int _randomDirection;
	private int _focusedDirection;
	private int _timeOfMouvement;
	private Vector3 _direction;
	private float _lastAttack;
	
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
	
	private void RandomMove()
	{
		_character.MovementSpeed = 2;
		
		// Si on passe en dessous de zéro, c'est que l'on doit relancer un compte
		if ( _changeDirection == 0 )
		{
			_randomDirection = _randomGenerator.Next( 1, 4 );
			_changeDirection = _randomGenerator.Next( 100, 300 );
			_timeOfMouvement = _randomGenerator.Next( 5, 100 );
		}
		
		
		// Choix de la _direction du movement (pour le moment on ne peut pas se déplacer en diagonale)
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
			Debug.Log( "ERREUR _direction" );
			break;
		}
		_timeOfMouvement--;
		if ( _direction != Vector3.zero && _timeOfMouvement > 0 )
		{
			_character.Move( _direction );
		}
		
		// On descend d'un tick
		_changeDirection--;
		
	}
	private void AggressiveMove()
	{
		_character.MovementSpeed = 3;
		
		// Si on peut taper
		if ( _direction.magnitude < 2 )
		{
			// Test si on est dans le bon tick 
            if ( _lastAttack + _character.AttackSpeed < Time.fixedTime )
            {
				_lastAttack=Time.fixedTime;
				_character.Attack();	
			}
			
			
		}
		// Sinon, on bouge vers le perso
		else if ( _direction != Vector3.zero  )
		{
			_character.Move( _direction );
		}
		
		
		
	}
	
	private bool DetectTarget()
	{
		GameObject player = GameObject.FindWithTag( "Player" );
		if ( player != null )
		{
			_direction = player.transform.position - _character.transform.position;
			if ( _direction.magnitude < _character._aggroArea )
			{
				return true;
			}
		}
		
		return false;
	}
	
	public Vector3 Direction()
	{
		return new Vector3();
	}
}
