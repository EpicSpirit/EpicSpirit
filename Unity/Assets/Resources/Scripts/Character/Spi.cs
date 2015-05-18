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
        float _lastReceivedDamage;

        Action comp;

        public override void Start()
        {
            base.Start();

            _health = 20;
            _lookAroundCount = 300;
            _attackCounter = 0;
            _comboAttackInterval = 0.9f;
            _dateOfLastAttack = Time.fixedTime;
            _lastReceivedDamage = 0f;

            _health = PlayerPrefs.GetInt("Spi_health");
            if ( _health == 0)
            {
                _health = 20;
                PlayerPrefs.SetInt( "Spi_health", _health );
            }



            /*
             * ########## NEW ##########
             * */
            //comp = new Sword( this );
        }


       
        public override void Update()
        {
            base.Update();
        }
       
        public override void Attack()
        {
            base.Attack();

            // Tick Management
            if ( isState( States.Attack ) && justAttacked)
            {
                justAttacked = false;
                comp.Act();
                StopAttack(comp.AttackDuration);
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

        private bool isInvincible () {
            return _lastReceivedDamage + 1f >= Time.fixedTime;
        }

        internal override void takeDamage ( int force )
        {
            if ( !isInvincible() )
            {
                _lastReceivedDamage = Time.fixedTime;

                base.takeDamage( force );
               
                AnimationManager( "damaged" );
                PlayerPrefs.SetInt( "Spi_health", _health );
            }
        }
    
    }
}