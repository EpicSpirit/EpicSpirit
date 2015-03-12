using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private MoveCharacter _moveCharacter;
    public GameObject _player;
	// Use this for initialization
	void Start () 
    {
        _moveCharacter = new MoveCharacter( _player );
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) )
        {
            _moveCharacter.MoveRight();
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.Q ) )
        {
            _moveCharacter.MoveLeft();
        }
        if ( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.Z ) )
        {
            _moveCharacter.MoveUp();
        }
        if ( Input.GetKey( KeyCode.DownArrow ) || Input.GetKey( KeyCode.S ) )
        {
            _moveCharacter.MoveDown();
        }

	}
}
