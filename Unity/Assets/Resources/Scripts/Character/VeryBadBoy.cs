using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class VeryBadBoy : Character
    {
        private static System.Random _randomGenerator = new System.Random();
        Action attack;

        public override void Start()
        {
            base.Start();

            _health = 30;
            _movementSpeed = 1;
            attack = this.gameObject.AddComponent<SummonBadBoy>();

        }

        public override void Attack()
        {
            // Pas de base.Attack()
            if ( ChangeState( States.Attack ) )
            {
                // Pour le moment notre méchant ne fait QUE Invoquer des BadBoy :-)
                attack.Act();
                StopAttack( attack.AttackDuration );
            }
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