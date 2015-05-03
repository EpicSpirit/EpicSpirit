﻿using System;
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

            // Gestion du tick
            if ( !isAttacking() )
            {

                //Loocking for the right attack phase 
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

                GetListOfTarget();
                foreach ( Character enemy in _targets )
                {
                    if ( enemy.name != this.name )
                    {
                        enemy.takeDamage( _attackCounter + 1 );
                    }
                }

                // Get the right attack
                if ( _attackCounter >= 2 )
                {
                    _attackCounter = 0;
                    AnimationManager( "attack_2" );
                }
                else
                {
                    AnimationManager( "attack" );
                }
            }

            Invoke( "StopAttack", 5.0f );
        }

        private void StopAttack () {
            Debug.Log( "Je suis un test" );
        }

        public override void Move( Vector3 direction )
        {
            base.Move( direction );
            if ( isState(State.Walk) )
            {
                AnimationManager( "walk" );
            }
            else if(isState(State.Idle)) {
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