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
            GameObject gameObject;
            if ( _character == null && (gameObject= GameObject.FindGameObjectWithTag( "Player" )) != null )
            {
                _character = gameObject.GetComponent<Character>();
            }
            if ( _movementJoystick == null && ( gameObject = GameObject.Find( "CNJoystick" ) )!= null )
            {
                _movementJoystick = gameObject.GetComponent<CNAbstractController>();
            }

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

			movement.y = 0f;

            _character.Move( movement );
        }

    }
}