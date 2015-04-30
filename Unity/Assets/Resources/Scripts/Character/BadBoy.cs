using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoy : Character
    {
        public override void Start()
        {
            base.Start();

            _health = 3;
            _movementSpeed = 2;

        }
        public override void Attack ()
        {
            base.Attack();
            AnimationManager("attack");
            
        }
        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( direction == Vector3.zero && !isAttacking())
            {
                AnimationManager( "idle" );
            }
            else if(!isAttacking())
            {
                AnimationManager( "walk" );
            }
        }
        internal override void takeDamage( int force )
        {
            base.takeDamage( force );
            AnimationManager( "damaged" );
        }

    }
}