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

                switch(_attackCounter) 
                {
                    case 0:
                        animationName = "attack";
                        break;
                    case 1:
                        animationName = "attack_2";
                        break;
                    case 2:
                        animationName = "attack_3";
                        _attackCounter = 0;
                        break;
                    default :
                        _attackCounter = 0;
                        animationName = "attack";
                        Debug.Log("Erreur dans la gestion du combo");
                        break;

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