using UnityEngine;
using System.Collections;

public class EnnemiIA : MonoBehaviour 
{
    public GameObject _ennemi;
    private Character _character;

    private static System.Random _randomGenerator= new System.Random();

    private int _changeDirection;
    private int _randomDirection;
	private int _timeOfMouvement;
	private Vector3 direction = new Vector3();


	// Use this for initialization
	void Start () 
    {
        _character = _ennemi.GetComponents<Character>()[0];
        _changeDirection = 0;
        _randomDirection = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {

		// Si on passe en dessous de zéro, c'est que l'on doit relancer un compte
        if (_changeDirection == 0)
        {
            _randomDirection = _randomGenerator.Next( 1, 4 );
            _changeDirection = _randomGenerator.Next(100, 300);
			_timeOfMouvement = _randomGenerator.Next (5, 100);
        }
        

		// CHoix de la direction du mouvement (pour le moment on ne peut pas se déplacer en diagonale)
		direction = Vector3.zero;
        switch ( _randomDirection )
        {
            case 1:
                direction += Vector3.right;
                break;
            case 2:
                direction += Vector3.left;
                break;
            case 3:
                direction += Vector3.forward;
                break;
            case 4:
                direction += Vector3.back;
                break;
			default :
				Debug.Log ("ERREUR direction");
				break;
        }
		_timeOfMouvement --;
        if ( direction != Vector3.zero && _timeOfMouvement > 0)
        {
            _character.Move( direction );
        }

		// On descend d'un tick
		_changeDirection--;
	
	}
}
