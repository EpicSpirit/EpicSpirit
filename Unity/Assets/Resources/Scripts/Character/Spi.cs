using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Spi : Character
    {
        int _attackCounter;
        float _dateOfLastAttack;
        float _comboAttackInterval;
        int _lookAroundCount;

        public override void Start()
        {
            base.Start();


            _health = 1000;
            _lookAroundCount = 300;
            _attackCounter = 0;
            _comboAttackInterval = 0.9f;
            _dateOfLastAttack = Time.fixedTime;
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Attack()
        {
            string animationName;
            base.Attack();
            // Gestion du tick
            if ( isState(States.Attack) && justAttacked)
            {
                justAttacked = false;
                //Looking for the right attack phase 
                if ( _dateOfLastAttack + _comboAttackInterval > Time.fixedTime )
                {
                    _attackCounter++;
                }
                else
                {
                    _attackCounter = 0;
                }
                // Update the last attack
                _dateOfLastAttack = Time.fixedTime;

                // Get the right attack
                if ( _attackCounter >= 2 )
                {
                    _attackCounter = 0;
                    animationName = "attack_2" ;
                }
                else
                {
                    animationName = "attack" ;
                }
                AnimationManager( animationName );
                StopAttack( animationName );
                
            }

            
        }

        

        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( isState(States.Walk) )
            {
                AnimationManager( "walk" );
            }
            else if(isState(States.Idle)) {
                if ( _animations && !_animations.IsPlaying( "look_around" ) )
                {
                    AnimationManager( "idle" );
                }

                // LookAround Management
                _lookAroundCount--;
                if ( _lookAroundCount < 0 )
                {
                    _lookAroundCount = 300;
                    AnimationManager( "look_around" );
                }
            }
        }
    
    }
}