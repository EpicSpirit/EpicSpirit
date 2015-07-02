using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{

    public abstract class Character : MonoBehaviour
    {
        #region Fields

        public short _movementSpeed;
        public short _attackRange;

        public short _aggroMovementSpeed;
        public int _aggroArea;
        public int _aggroAreaAfterFirstAggro;
        public bool _followEveryWhereAfterFirstAggro;

        public int _currentHealth;
        public int _maxHealth;

        internal AudioSource _audioSource;
        

        // Character Statistic
       // internal float _speedRotation;
        internal int _attack;
        internal float _attackSpeed = 5f;

        // Utilities
        internal RaycastHit _hit;
        internal Animation _animations;
        internal List<Character> _targets;
        internal List<Vector3> _attackVectors;

        // Controller
        private CharacterController _characterController;

        // Attack
        internal List<Action> _actions;

        #endregion

        #region Properties
        public CharacterController CharacterController
        {
            get { return _characterController; }
        }
        public AudioSource AudioSource
        {
            get
            {
                return _audioSource;
            }
            set
            {
                _audioSource = value;
            }
        }
        public short MovementSpeed
        {
            set { _movementSpeed = value; }
            get { return _movementSpeed; }
        }
        public float AttackSpeed
        {
            set { _attackSpeed = value; }
            get { return _attackSpeed; }
        }

        public virtual int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }
        public virtual int CurrentHealth
        {
            get { return _currentHealth; }
            set 
            {
                if ( value <= 0 && !_dead) { Die(); _dead = true; }
                _currentHealth = value;
            }
        }

        #endregion

        public virtual void Awake()
        {
            InitializeAnimationManager();

            _actions = new List<Action>();
            _currentHealth = 3;
            _dead = false;

            _characterController = this.GetComponent<CharacterController>();
            AudioSource = this.GetComponent<AudioSource>();
            if ( _characterController == null )
            {
                throw new NullReferenceException( "Character must have a CharacterController" );
            }

            InitializeStateManager();
        }

        public virtual void Start ()
        {

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

        public virtual void Move ( Vector3 direction )
        {
            direction.y = 0;
            if ( _effect != Effect.None )
            {
                _animations.Stop();
                return; 
            }
            
            else if ( direction != Vector3.zero && ChangeState(States.Walk))
            {
                //_characterController.transform.rotation = Quaternion.LookRotation( direction );
                //_characterController.Move( direction * _movementSpeed * Time.deltaTime );
                //this.transform.rotation = Quaternion.Lerp( this.transform.rotation, Quaternion.LookRotation( direction ), 5 * Time.deltaTime );
               
                this.transform.rotation = Quaternion.Lerp( this.transform.rotation, Quaternion.LookRotation( direction ), 18 * Time.deltaTime );
                _characterController.Move( direction * _movementSpeed * Time.deltaTime );

                
            } 
            else if( direction == Vector3.zero && State <= States.Walk) 
            {
                EndOfState();
            }
            else
            {
                
                // Nothing 
                // TEST
                
            }
        }
        /// <summary>
        /// Move the character to destination. This method have to be called on all updates.
        /// </summary>
        /// <param name="destination">The destination to go</param>
        /// <returns>Return true if the character is moving</returns>
        public bool MoveTo(Vector3 destination)
        {
            Vector3 direction = new Vector3();
            // Change magicNumer if character destination is incorrect
            float magicNumber = 0.2f;
            if(
                ( ( destination.x - transform.position.x ) >= magicNumber ) || ( ( destination.x - transform.position.x ) <= -magicNumber ) ||
                ( ( destination.z - transform.position.z ) >= magicNumber ) || ( ( destination.z - transform.position.z ) <= -magicNumber ) 
                )
            {
                direction = destination - transform.position;
            }
            else
            {
                direction = Vector3.zero;
            }
            direction.Normalize();
            direction *= 4;
            Move( direction );
            return !( direction == Vector3.zero );
        }

        /// <summary>
        /// Move the character on a side (x axis).
        /// </summary>
        /// <param name="speed">Movement distance (on x axis)</param>
        /// <returns>True</returns>
        public bool MoveAsideTo( float speed )
        {
            float destination = 0f;
            Vector3 direction = new Vector3();
            direction = transform.position;
            // Change magicNumer if character destination is incorrect
            float magicNumber = 0.2f;
            if (( ( destination - transform.position.x ) >= magicNumber ) || ( ( destination - transform.position.x ) <= -magicNumber ) )
            {
                direction.x = destination - transform.position.x;
            }
            else
            {
                direction = Vector3.zero;
            }
            direction.Normalize();
            direction *= 4;
            CharacterController.Move( direction * speed * Time.deltaTime );
            return !( direction == Vector3.zero );
        }

        // TODO: Mettre en place les vecteur d'attaque en fonction du attack_range
        // TODO: Gérer la force d'attaque
        // TODO : Invoke( "StopAttack", _animations.GetClip( "attack" ).length) Rendre ça générique pour les différentes animations 
        public void Attack () 
        {
            Attack(0);
        }
        Action _actualAction;
        public void Attack (int indice) 
        {
            
            // Tick Management
            if ( ChangeState( States.Attack ) )
            {
                
                _actualAction = _actions [indice];
                if ( _actualAction.Act() )
                {
                    
                    StopAttack( _actualAction.AttackDuration );
                }
                else
                {
                    StopAttack( 0f );
                }
            }
        }

        public Action GetAttack (int indice) 
        {

            return _actions[indice];
        }
        
        /*
        private void MoveBack() 
        {
            _enemy.GetComponent<CharacterController>().Move( ( _enemy.transform.position - this.transform.position ) * 5 * Time.deltaTime );
        }
         * */

        internal void StopAttack ( float duration )
        {
            
            Invoke( "EndOfState", duration );
        }

        // TODO : Rendre ça plus propre au niveau algo
        // TODO : Faire la migration dans le particule manager
        internal virtual void takeDamage ( int force )
        {
            if ( isState( States.Attack ) )
            {

                if ( !_actualAction.IsStoppable ) return;
                else _actualAction.CancelAttack();
            }
            


            if ( ChangeState( States.Damaged ) )
            {
                ParticuleManager( "DamageEffect" );
                CurrentHealth -= force;
                Invoke( "EndOfState", 0.3f );
                

            }

        }

        internal virtual void Die()
        {
            GameObject.Destroy( this.gameObject, 0.5f );

            if ( GameObject.FindWithTag( "Player" ).GetComponent<Character>() == this )
                Application.LoadLevel( "game_over" );
        }

        public void MoveBack(Vector3 v)
        {
            this.GetComponent<CharacterController>().Move( v * Time.deltaTime );
        }

        
		public virtual void MoveBack(GameObject c, float strengh)
        {
            MoveBack( ( this.transform.position - c.transform.position ).normalized * strengh);
        }

        public void MoveBack(float strengh)
        {
            MoveBack( this.transform.TransformDirection( Vector3.back * strengh ));
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
                _animations.CrossFade( anim, 0.1f );
                //_animations.Play( anim );
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

        public enum States : int
        {
            Idle = 1,
            Walk = 2,
            Attack = 4,
            Damaged = 8,
        }
        States _state;
        List<int> mask;

        internal void InitializeStateManager ()
        {
            _state = States.Idle;
            mask = new List<int>();
            mask.Add( 15 ); // IDLE
            mask.Add( 14 ); // WALK
            mask.Add( 8 );  // ATTACK
            mask.Add( 8 );  // DAMAGED

        }

        internal void EndOfState ()
        {
            _actualAction = null;
            _state = States.Idle;
        }

        internal States State
        {
            get { return _state; }
            set { _state = value; }
        }


        /// <summary>
        /// Return true if the state is right
        /// </summary>
        /// <param name="state">The state to match</param>
        /// <returns>True if right</returns>
        public bool isState ( States state )
        {
            if ( state == null ) { throw new ArgumentNullException(); }

            return _state == state;
        }

        /// <summary>
        /// Change State if it's possible
        /// </summary>
        /// <param name="state">The new state</param>
        /// <returns>True if the state has changed</returns>
        public bool ChangeState ( States state )
        {
            if ( state == null ) { throw new ArgumentNullException(); }

            // EffectGestion
            if ( _effect != Effect.None && state != States.Damaged) return false;

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
        private bool isPriority ( States currentState, States newState )
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
        private  Effect _effect;
        private  bool _dead;

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

        #region EffectManagment
        internal void Iced ( float time )
        {
            _effect = Effect.Iced;
            Invoke( "ReturnNormalState", time );
        }
        private void ReturnNormalState()
        {
            _effect = Effect.None;
        }

        public enum Effect
        {
            None,
            Iced
        }
        #endregion
    
    }
}