using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class SpiNaked : Character
    {
        SaveManager _saveManager;

        float _dateOfLastAttack;
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

            _dateOfLastAttack = Time.fixedTime;
            _lastReceivedDamage = 0f;

            _currentHealth = 3;

        }

        public override void Move ( Vector3 direction )
        {
            base.Move( direction );

            if ( isState( States.Walk ) )
            {
                AnimationManager( "walk" );
            }
            else if ( isState( States.Idle ) )
            {
                if ( _animations )
                {
                    AnimationManager( "idle" );
                }
            }

        }

        private bool isInvincible ()
        {
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