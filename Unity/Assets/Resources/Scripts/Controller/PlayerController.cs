using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IController
{
    public bool _joystickOn;
    
    public Character _character;

    // Joystick controller
    public CNAbstractController _movementJoystick;

    // Main camera transform used for joystick
    private Transform _mainCameraTransform;

	// Use this for initialization
	void Start () 
    {
        _mainCameraTransform = Camera.main.GetComponent<Transform>();    
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ( _character != null )
        {
            if (_joystickOn)
            {
                JoystickMove();
            }
            else
            {
                KeyboardMove();
                Attack();
            }
    	}
	}


    private void Attack()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            _character.Attack();
        }
    }
    private void KeyboardMove()
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
    }

    private void JoystickMove()
    {
        var movement = new Vector3(
            _movementJoystick.GetAxis( "Horizontal" ),
            0f,
            _movementJoystick.GetAxis( "Vertical" ) );

        movement = _mainCameraTransform.TransformDirection( movement );
        movement.y = 0f;
        movement.Normalize();

        _character.Move( movement );
    }
	public Vector3 Direction()
    {
        return new Vector3();
    }

}
