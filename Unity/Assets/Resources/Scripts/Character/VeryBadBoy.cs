using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class VeryBadBoy : Enemy
    {
        private static System.Random _randomGenerator = new System.Random();
        VeryBadBoyAI AI;
        GameObject _bouclier;


        public override void Awake ()
        {
            base.Awake();

            _currentHealth = 30;
			_aggroArea = int.MaxValue;
			_aggroMovementSpeed = 7;
            _actions.Add( this.gameObject.AddComponent<SummonBadBoy>() );
            _actions.Add( this.gameObject.AddComponent<TurnAround>() );
            _actions.Add( this.gameObject.AddComponent<IceJail>() );
            _actions.Add( this.gameObject.AddComponent<ChargeTowardEnemy>() );

            AI = GetComponent<VeryBadBoyAI>();
            _bouclier = GameObject.Find( "Bouclier" );
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            _bouclier.transform.RotateAroundLocal( Vector3.down, 5f*Time.deltaTime );
        }


        internal override void takeDamage ( int force, Action actionAttacker )
        {
            if ( !AI.isInvincible )
            {
                base.takeDamage( force, actionAttacker );
                if ( isState( States.Damaged ) )
                {
                    AnimationManager( "damaged" );
                }
                if ( _currentHealth <= 0 )
                {
                    Invoke( "LoadLevel", 1f );
                }
            }
        }


        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( isState( States.Idle ) )
            {
                AnimationManager( "idle" );
            }
            else if ( isState( States.Walk ) )
            {
                AnimationManager( "walk" );
            }
        }

        internal void Stunt ()
        {
            ChangeState( States.Damaged );
            Invoke( "EndOfState", 2f );
        }
    }
}