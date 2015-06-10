using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoy : Enemy
    {
        Action attack;

        public override void Awake ()
        {
            base.Awake();

            _health = 3;
            _actions.Add( this.gameObject.AddComponent<Tacle>() );

        }

        public override void Start()
        {
            base.Start();
        }
        

        //Todo : Même code que VeryBadBoy, rendre ça Dry avec une méthode générique ?
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
        internal override void takeDamage( int force )
        {
            base.takeDamage( force );

            if ( isState( States.Damaged ) )
            {
                AnimationManager( "damaged" );
            }
        }

    }
}