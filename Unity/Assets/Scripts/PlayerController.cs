using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject _player;
	private Character _character;

	// Use this for initialization
	void Start () 
    {

		_character = _player.GetComponents<Character>()[0];
        _character.Speed = 15;
    }
	
	// Update is called once per frame
	void Update ()
    {
		Vector3 direction = new Vector3();
		// Test pour la gestion des touches pour le moment
        if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) )
        {
			direction += Vector3.right;
		}
        if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.Q ) )
        {
			direction += Vector3.left;

		}
        if ( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.Z ) )
        {
			direction += Vector3.forward;

		}
        if ( Input.GetKey( KeyCode.DownArrow ) || Input.GetKey( KeyCode.S ) )
        {
            direction += Vector3.back;
		}

		// Application de la méthode Move si on doit bouger
        _character.Move( direction );

        if ( Input.GetKeyDown( KeyCode.Space ) ) 
        {

			_character.Attack();
		}





	}
}
