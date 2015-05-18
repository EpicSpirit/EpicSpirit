using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoy : Character
    {
        Action attack;
        public override void Start()
        {
            base.Start();

            _health = 3;
            attack = this.gameObject.AddComponent<Tacle>();

        }
        public override void Attack ()
        {
            if ( ChangeState( States.Attack ))
            {
                attack.Act();
                StopAttack(attack.AttackDuration);
            }
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