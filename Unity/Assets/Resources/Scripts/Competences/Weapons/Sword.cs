﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class Sword : Weapon
    {
        int _currentPhase;

        // TMP
        public void Awake()
        {
            _image = Resources.Load<Sprite>( "UI/Images/SimpleSword" );
        }

        public override void Start ()
        {
            base.Start();
            Debug.Log("Chargement epee");
            // _animation is null if we aren't in Character (for Progression Manager for example.)
            if(_animation != null) 
            {
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_1", _animation.GetClip( "SimpleSword_1" ).length / 2 ) );
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_2", _animation.GetClip( "SimpleSword_2" ).length / 2 ) );
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_3", _animation.GetClip( "SimpleSword_3" ).length / 2 ) );
            }

            _currentPhase = 0;
            _attackDuration = 1;
            _strengh = 1;
            _image = Resources.Load<Sprite>( "UI/Images/SimpleSword" );
            _isStoppable = false;
        }

        public override float AttackDuration
        {
            get { return _animation.GetClip( _attackAnimations [_currentPhase].AnimationName ).length; }
        }

        public override void Act ()
        {
            _character.AnimationManager( _attackAnimations[_currentPhase].AnimationName );
            Invoke( "Damage", _attackAnimations[_currentPhase].TimeAttack );
        }

        public void Damage () 
        {
            List<Character> targets = GetListOfTarget();
            foreach(Character character in targets)
            {
                character.takeDamage( _strengh );
            }
            _currentPhase ++;
            if ( _currentPhase > 2 ) _currentPhase = 0;
        }
    }
}