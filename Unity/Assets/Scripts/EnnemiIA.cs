using UnityEngine;
using System.Collections;

public class EnnemiIA : MonoBehaviour 
{
    public GameObject _ennemi;
    private Character _character;

    private System.Random _randomGenerator;

    private int _changeDirection;
    int _randomDirection;

	// Use this for initialization
	void Start () 
    {
        _character = _ennemi.GetComponents<Character>()[0];
        _randomGenerator = new System.Random();
        _changeDirection = 0;
        _randomDirection = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_changeDirection == 0)
        {
            _randomDirection = _randomGenerator.Next( 1, 4 );
            _changeDirection = 100;
        }
        

        Vector3 direction = new Vector3();



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
        }
        if ( direction != Vector3.zero )
        {
            _character.Move( direction );
            _changeDirection--;
        }
	
	}
}
