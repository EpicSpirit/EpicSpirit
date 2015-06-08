using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Spi : Character
    {
        SaveManager _saveManager;

        int _attackCounter;
        float _dateOfLastAttack;
        float _comboAttackInterval;
        int _lookAroundCount;
        float _lastReceivedDamage;

        public virtual int Health
        {
            get { return _health; }
            set
            {
                if ( value < 0 ) throw new InvalidOperationException( "health can't be negative" );
                SaveManager.SaveSpiHealth( _health );
                _health = value;
            }
        }

        public override void Awake ()
        {
            base.Awake();

            _lookAroundCount = 300;
            _attackCounter = 0;
            _comboAttackInterval = 0.9f;
            _dateOfLastAttack = Time.fixedTime;
            _lastReceivedDamage = 0f;

            _health = SaveManager.GetSpiHealth();
            if ( Health == 0 )
            {
                Health = 20;
            }

            // Pour le moment on n'a pas encore de menu pour les comp donc on les rajoute manuellement
            _actions.Add( this.gameObject.AddComponent<Sword>() );
            _actions.Add( this.gameObject.AddComponent<HealthPotion>() );
            _actions.Add( this.gameObject.AddComponent<FireBall>() );
            _actions.Add( this.gameObject.AddComponent<Dodge>() );

        }

        public override void Start()
        {
            base.Start();
        }

        
       
        public override void Update()
        {
            base.Update();

        }

        public override void Move( Vector3 direction )
        {
            base.Move( direction );


            if ( isState(States.Walk) )
            {
                AnimationManager( "walk" );
            }
            else if(isState(States.Idle)) 
            {
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
            }
        }
    
    }
}