using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{
    public abstract class Character : MonoBehaviour
    {
        #region Fields

        public float _movementSpeed;
        public float _attackRange;

        // Character Statistic
        internal float _speedRotation;
        internal int _attack;
        internal int _health;
        internal float _attackSpeed = 5f;

        // Utilities
        internal RaycastHit _hit;
        internal Animation _animations;
        internal List<Character> _targets;
        internal List<Vector3> _attackVectors;

        // Controller
        private CharacterController _characterController;

        #endregion

        #region Properties
        public float MovementSpeed
        {
            set { _movementSpeed = value; }
            get { return _movementSpeed; }
        }
        public float AttackSpeed
        {
            set { _attackSpeed = value; }
            get { return _attackSpeed; }
        }

        #endregion

        public virtual void Start ()
        {
            InitializeAnimationManager();

            _health = 3;
            _attackVectors = new List<Vector3>();
            _attackVectors.Add( new Vector3( 0, 1, 2 ) );
            _attackVectors.Add( new Vector3( -1, 1, 2 ) );
            _attackVectors.Add( new Vector3( 1, 1, 2 ) );

            _characterController = this.GetComponent<CharacterController>();
            if ( _characterController == null )
            {
                throw new NullReferenceException( "Character must have a CharacterController" );
            }

            // InitializeStateManager();

        }

        public virtual void Update ()
        {
            Gravity();
        }

        public void Gravity ()
        {
            // Gravité // NON ! // Si ! C'est juste pour y aller Yolo pour le moment :p
            if ( !_characterController.isGrounded )
            {
                _characterController.Move( Vector3.down );
            }
        }

        private void InitializeAnimationManager ()
        {
            _animations = this.GetComponent<Animation>();
            if ( _animations == null )
            {
                _animations = this.GetComponentInChildren<Animation>();
            }

            if ( _animations == null )
            {
                throw new NullReferenceException( "Character must have Animation Component" );

            }
        }

        // TODO : Factoriser cette méthode, tout les perso n'ont pas un look around
        public virtual void Move ( Vector3 direction )
        {
            direction.y = 0;
            if ( !isAttacking() && direction != Vector3.zero )
            {
                _characterController.Move( direction * _movementSpeed * Time.deltaTime );
                _characterController.transform.rotation = Quaternion.LookRotation( direction );
            }
        }

        internal bool isAttacking ()
        {
            if ( _animations )
            {
                return _animations.IsPlaying( "attack" ) || _animations.IsPlaying( "attack_2" );
            }
            else
            {
                return false;
            }
        }

        // TODO: Mettre en place les vecteur d'attaque en fonction du attack_range
        // TODO: Gérer la force d'attaque
        public virtual void Attack ()
        {
            // Tick Management
            if ( !isAttacking() )
            {
                GetListOfTarget();
                foreach ( Character enemy in _targets )
                {
                    enemy.takeDamage( 1 );
                }

                AnimationManager( "attack" );
            }

        }

        // TODO : remettre l'origine de l'attaque au bon endroit
        internal List<Character> GetListOfTarget ()
        {
            _targets = new List<Character>();
            Vector3 realAttackOrigin = new Vector3( transform.position.x, transform.position.y + 1, transform.position.z );

            foreach ( Vector3 vector in _attackVectors )
            {
                Debug.DrawRay( realAttackOrigin, this.transform.TransformDirection( vector ), Color.yellow, 1.0f );

                if ( Physics.Raycast( realAttackOrigin, this.transform.TransformDirection( vector ), out _hit ) )
                {

                    Character target = null;
                    target = _hit.transform.GetComponent<Character>();

                    if ( target != null && target.name != this.name )
                    {
                        _targets.Add( target );
                    }
                }
            }
            return _targets;

        }

        // TODO: Rendre ça plus propre au niveau algo
        // TODO : Faire la migration dans le particule manager
        internal virtual void takeDamage ( int force )
        {
            ParticleSystem[] particuleSystems = this.GetComponentsInChildren<ParticleSystem>();
            foreach ( ParticleSystem particuleSystem in particuleSystems )
            {
                if ( particuleSystem.name == "DamageEffect" )
                {
                    particuleSystem.Play();
                }
            }

            _health -= force;

            if ( _health <= 0 )
            {
                GameObject.Destroy( this.gameObject, 0.5f );
            }

        }

        #region AnimationManager
        public void AnimationManager ( string anim )
        {
            if ( _animations.GetClip( anim ) == null )
            {
                Debug.Log( "Can't find the animation " + anim + " in " + this.name );
                return;
            }

            if ( _animations != null && !_animations.IsPlaying( anim ) )
            {
                _animations.Play( anim );
            }
        }
        #endregion

        #region ParticuleManager
        // TODO : Rendre ça opti
        public void ParticuleManager ( string name )
        {
            ParticleSystem[] particulesSystem = this.GetComponentsInChildren<ParticleSystem>();

            foreach ( ParticleSystem particuleSystem in particulesSystem )
            {
                if ( name == particuleSystem.name )
                {
                    particuleSystem.Play();
                }

            }


        }
        #endregion

        #region StateManager

        public enum State : int
        {
            Idle = 1,
            Walk = 2,
            Attack = 4,
            Damaged = 8,
            Dead = 16,
            Cinematic = 32
        }
        State _state;
        List<int> mask;

        public void InitializeStateManager ()
        {
            _state = State.Idle;
            mask [0] = 32; // Idle          100000
            mask [1] = 48; // Walk          110000
            mask [2] = 48; // Attack        110000
            mask [3] = 56; // Damaged       111000
            mask [4] = 62; // Dead          111110
            mask [5] = 63; // Cinematic     111111
        }

        /// <summary>
        /// Return true if the state is right
        /// </summary>
        /// <param name="state">The state to match</param>
        /// <returns>True if right</returns>
        public bool isState ( State state )
        {
            if ( state == null ) { throw new ArgumentNullException(); }

            return _state == state;
        }

        /// <summary>
        /// Change State if it's possible
        /// </summary>
        /// <param name="state">The new state</param>
        /// <returns>True if the state has changed</returns>
        public bool ChangeState ( State state )
        {
            if ( state == null ) { throw new ArgumentNullException(); }

            if ( isPriority( _state, state ) )
            {
                _state = state;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compare the priority of both states
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="newState"></param>
        /// <returns>True if the new state is priority than the current state</returns>
        private bool isPriority ( State currentState, State newState )
        {
            if ( currentState == null || newState == null ) throw new NullReferenceException( "Parameters can't be null" );

            return ( mask [Log2ForPower2( ( UInt32 ) currentState )] & ( int ) newState ) != 0;
        }
#endregion

        #region LogCalul_ThanksToCKCORE
        static int[] _multiplyDeBruijnBitPosition = 
				{ 
					0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 
					31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 
				};

        /// <summary>
        /// Compute the Log2 (logarithm base 2) of a given number.
        /// </summary>
        /// <param name="v">Integer to compute</param>
        /// <returns>Log2 of the given integer</returns>
        [CLSCompliant( false )]
        static public int Log2 ( UInt32 v )
        {
            unchecked
            {
                v |= v >> 1;
                v |= v >> 2;
                v |= v >> 4;
                v |= v >> 8;
                v |= v >> 16;
                v = ( v >> 1 ) + 1;
                return _multiplyDeBruijnBitPosition [( ( v * 0x077CB531U ) ) >> 27];
            }
        }

        /// <summary>
        /// Compute the Log2ForPower2 (logarithm base 2 power 2) of a given number.
        /// </summary>
        /// <param name="v">Integer to compute. It MUST be a power of 2.</param>
        /// <returns>Result</returns>
        [CLSCompliant( false )]
        static public int Log2ForPower2 ( UInt32 v )
        {
            unchecked
            {
                return _multiplyDeBruijnBitPosition [( ( ( uint ) v * 0x077CB531U ) ) >> 27];
            }
        }
        #endregion

    }
}