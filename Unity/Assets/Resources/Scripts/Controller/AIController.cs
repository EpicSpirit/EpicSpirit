using System;
using UnityEngine;

namespace EpicSpirit.Game
{
    //Todo : créer une classe spécifique pour tout les ennemis
    public class AIController : MonoBehaviour
    {
        #region Fields
        private float _randomMovementSpeed;

        internal Enemy _character;
        private static System.Random _randomGenerator = new System.Random();
        public GameObject Target;



        // Delay until the next move
        private int _changeDirection;
        private int _randomDirection;

        // Time interval for the current move
        private int _timeOfMovement;
        private Vector3 _direction;

        // Time of the last Attack
        private float _lastAttack;
        #endregion
        internal OnInvisibleOptimization a;


        internal virtual void Awake ()
        {
            _direction = new Vector3();
            _lastAttack = Time.fixedTime;
            _character = GetComponent<Enemy>();
            _changeDirection = 0;
            _randomDirection = 1;

            if ( _character.IsSleeping )
                _character.MovementSpeed = 0;

            _randomMovementSpeed = _character._movementSpeed;

        }

        void Start()
        {
        }

        public virtual void Update()
        {
            if ( DetectTarget() || Target !=null)
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

            if ( !_character.IsSleeping )
            {
                _character.MovementSpeed = _randomMovementSpeed;

                //Move if direction is set
                if ( _timeOfMovement > 0 )
                {
                    _character.Move( _direction );
                }
            }
            _changeDirection--;

        }

        private bool DetectTarget()
        {
            GameObject player = GameObject.FindWithTag( "Player" );
            if ( player != null )
            {
                _direction = player.transform.position - _character.transform.position;
                if ( _direction.magnitude <= _character._aggroArea )
                {
                    return true;
                }
            }

            return false;
        }

        internal virtual void AggressiveMove()
        {
            // Add range to aggro area after first aggro
            if(_character._followEveryWhereAfterFirstAggro)
            {
                _character._aggroArea = int.MaxValue;
            }
            else
            {
                _character._aggroArea = _character._aggroAreaAfterFirstAggro;
            }

            if ( !_character.IsSleeping )
            {
                _character.MovementSpeed = _character._aggroMovementSpeed;

                // Attack the enemy if he is near
                if ( _direction.magnitude < _character._attackRange )
                {
                    if ( _lastAttack + _character.AttackSpeed < Time.fixedTime )
                    {
                        _lastAttack = Time.fixedTime;
                        _character.Attack();
                    }
                }
            }
            _character.Move( Vector3.ClampMagnitude( _direction, 1 ) );

        }
    }
}