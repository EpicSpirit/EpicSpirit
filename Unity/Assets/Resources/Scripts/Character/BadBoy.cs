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
            if(isState(State.Attack) &&  justAttack) {
                justAttack = false;
                AnimationManager( "attack" );
            }
            
        }
        //Todo : Même code que VeryBadBoy, rendre ça Dry avec une méthode générique ?
        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if(isState(State.Idle)) {
                AnimationManager( "idle" );
            }
            else if(isState(State.Walk))
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