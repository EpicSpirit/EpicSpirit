﻿using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class VeryBadBoy : Character
    {
        private static System.Random _randomGenerator = new System.Random();


        public override void Start()
        {
            base.Start();

            _health = 30;
            _movementSpeed = 0.5f;

        }

        public override void Attack()
        {
            
            if(ChangeState(State.Attack))
            {
                // Pour le moment notre méchant ne fait QUE Invoquer des BadBoy :-)
                SummonBadBoy();
                Invoke( "StopAttack", _animations.GetClip( "invoke" ).length );
            }

            

        }

        // TODO : Revoir l'histoire du random pour avoir une bonne plage de valeur
        // TODO : Rendre la méthode un peu plus générique ? 
        private void SummonBadBoy()
        {
            // Play the animation
            AnimationManager( "invoke" );

            Invoke( "RealSummon", _animations.GetClip( "invoke" ).length - 0.5f );
            
        }
        private void RealSummon() {
            // Real attack
            Vector3 position = this.transform.position;

            position.x += _randomGenerator.Next( 1, 5 );
            position.z += _randomGenerator.Next( 1, 5 );

            GameObject badBoy = Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/BadBoy" ), position, this.transform.rotation ) as GameObject;
            Character enemy = badBoy.GetComponent<Character>();
            enemy.ParticuleManager( "Invokation" );
        }

        //Todo : Même code que BadBoy, rendre ça Dry avec une méthode générique ?
        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( isState( State.Idle ) )
            {
                AnimationManager( "idle" );
            }
            else if ( isState( State.Walk ) )
            {
                AnimationManager( "walk" );
            }
        }
    }
}