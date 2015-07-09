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

        public virtual int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                SaveManager.SaveSpiHealth( _currentHealth );
                _currentHealth = value;
            }
        }

        public override void Awake ()
        {

            base.Awake();

            //GameObject.Find( "SaveManager" ).GetComponent<SaveManager>().ResetSave();

            _lookAroundCount = 300;
            _attackCounter = 0;
            _comboAttackInterval = 0.9f;
            _dateOfLastAttack = Time.fixedTime;
            _lastReceivedDamage = 0f;

            _currentHealth = SaveManager.GetSpiHealth();
            _maxHealth = 20;
            if ( CurrentHealth == 0 )
            {
                CurrentHealth = 20;
            }

            _actions = SaveManager.LoadAction();

            if ( _actions != null )
            {
                for ( int i=0; i < _actions.Count; i++ )
                {
                    _actions [i] = _actions [i].AddActionToPerso( this.gameObject );

                }
            }

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

        internal override void takeDamage ( int force, Action actionAttacker )
        {
            if ( !isInvincible() )
            {
                _lastReceivedDamage = Time.fixedTime;
                base.takeDamage( force, actionAttacker );
                AnimationManager( "damaged" );
            }
        }
    
    }
}