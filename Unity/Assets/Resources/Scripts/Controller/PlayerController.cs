using UnityEngine;
using System.Collections;
using System;

namespace EpicSpirit.Game
{
    [RequireComponent( typeof( CharacterController ) )]
    public class PlayerController : MonoBehaviour
    {
        // Switch for keyboard/Joystick Controller
        public bool _joystickOn;

        // Character that we have to control
        public Character _character;

        // Joystick controller
        public CNAbstractController _movementJoystick;

        // Main camera transform used for joystick
        private Transform _mainCameraTransform;

        void Awake ()
        {
            _mainCameraTransform = Camera.main.GetComponent<Transform>();
            if ( _character == null ) _character = GameObject.Find( "Spi" ).GetComponent<Character>();
            if ( _movementJoystick == null ) _movementJoystick = GameObject.Find( "CNJoystick" ).GetComponent<CNAbstractController>();

        }
        void Start()
        {
        }

        void Update()
        {
            if ( _character != null )
            {
				if (Application.platform == RuntimePlatform.Android || _joystickOn )
                {
                    JoystickMove();
                }
                else
                {
                    KeyboardMove();
                    Attack();
                }
            }
            else
            {
                throw new Exception( "I need the character" );
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

            _character.Move( direction );
        }

        private void JoystickMove()
        {
			var movement = new Vector3 (
                _movementJoystick.GetAxis ("Horizontal"),
                0f,
                _movementJoystick.GetAxis ("Vertical"));

			//movement = _mainCameraTransform.TransformDirection (movement);
			movement.y = 0f;
			//  movement.Normalize();
		// Si le devant du perso est différent de la direction empruntée, faire une rotation en fonction du temps à la place du déplacement.

            _character.Move( movement );
        }

    }
}