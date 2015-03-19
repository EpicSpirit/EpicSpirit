using UnityEngine;
using System.Collections;

public class EnnemiIA : MonoBehaviour 
{
    public int _aggroArea;

    private Character _character;

    private static System.Random _randomGenerator= new System.Random();

    private int _changeDirection;
    private int _randomDirection;
    private int _focusedDirection;
	private int _timeOfMouvement;
	private Vector3 _direction = new Vector3();


	// Use this for initialization
	void Start () 
    {
        if (_aggroArea == 0)
        {
            _aggroArea = 8;
        }
        _character = this.GetComponents<Character>()[0];
        _character.Speed = 2;
        _changeDirection = 0;
        _randomDirection = 1;

	}
	
	// Update is called once per frame
	void Update () 
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
        _character.Speed = 2;

        // Si on passe en dessous de zéro, c'est que l'on doit relancer un compte
        if ( _changeDirection == 0 )
        {
            _randomDirection = _randomGenerator.Next( 1, 4 );
            _changeDirection = _randomGenerator.Next( 100, 300 );
            _timeOfMouvement = _randomGenerator.Next( 5, 100 );
        }


        // Choix de la _direction du mouvement (pour le moment on ne peut pas se déplacer en diagonale)
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
        _character.Speed = 3;

        if ( _direction != Vector3.zero )
        {
            _character.Move( _direction );
        }
        if (_direction.magnitude < 2)
        {
            Vector3 cone_centre;
            cone_centre.z = 2;
            cone_centre.x = 0;
            cone_centre.y = 0;
            RaycastHit hit;
	        Character adv;
            if ( Physics.Raycast( transform.position, transform.TransformDirection( cone_centre ), out hit ) )
            {
                adv = null;
                adv = hit.transform.GetComponent<Character>();
                if ( adv != null && adv.Life >= 0)
                {
                    WaitForEndOfFrame a = new WaitForEndOfFrame();
                    adv.takeDamage();
                }
            }
        }
        
        

    }

    private bool DetectTarget()
    {
        GameObject player = GameObject.FindWithTag( "Player" );
        if ( player != null )
        {
            _direction = player.transform.position - this.transform.position;
            if ( _direction.magnitude < _aggroArea )
            {
                return true;
            }
        }
        
        return false;
    }
}
