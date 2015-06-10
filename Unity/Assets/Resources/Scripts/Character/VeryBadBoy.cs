using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class VeryBadBoy : Enemy
    {
        private static System.Random _randomGenerator = new System.Random();

        public override void Awake ()
        {
            base.Awake();

            _health = 30;
            _movementSpeed = 1;
            _actions.Add( this.gameObject.AddComponent<SummonBadBoy>() );

        }

        public override void Start()
        {
            base.Start();

            
        }
        internal override void takeDamage ( int force )
        {
            base.takeDamage( force );

            if ( isState( States.Damaged ) )
            {
                AnimationManager( "damaged" );

            }
            if(_health <= 5) 
            {
                Invoke("LoadLevel", 1f);
            }
        }

        void LoadLevel ()
        {
            Application.LoadLevel( "endstory" );
        }

        //Todo : Même code que BadBoy, rendre ça Dry avec une méthode générique ?
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
    }
}