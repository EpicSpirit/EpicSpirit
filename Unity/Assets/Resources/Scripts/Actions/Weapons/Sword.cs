using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class Sword : Weapon
    {
        int _currentPhase;

        // TMP
        public override void Awake()
        {
            base.Awake();

            // _animation is null if we aren't in Character (for Progression Manager for example.)
            if ( _animations != null )
            {
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_1", _animations.GetClip( "SimpleSword_1" ).length / 2 ) );
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_2", _animations.GetClip( "SimpleSword_2" ).length / 2 ) );
                _attackAnimations.Add( new AttackAnimation( "SimpleSword_3", _animations.GetClip( "SimpleSword_3" ).length / 2 ) );
            }

            _currentPhase = 0;
            _attackDuration = 1;
            _strengh = 1;
            _image = Resources.Load<Sprite>( "UI/Images/SimpleSword" );
            _isStoppable = false;
            _name = "Sword";
			_description = "Although it comes from the forest heart, this blade seems banal.";
			_range = 3;
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<Sword>();
        }

        public override void Start ()
        {
            base.Start();
        }

        public override float AttackDuration
        {
            get { return _animations.GetClip( _attackAnimations [_currentPhase].AnimationName ).length; }
        }

        public override bool Act ()
        {
            if ( base.Act() )
            {
                _character.AnimationManager( _attackAnimations [_currentPhase].AnimationName );
                Invoke( "Damage", _attackAnimations [_currentPhase].TimeAttack );
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Damage () 
        {
            List<Character> targets = GetListOfTarget();
            foreach(Character character in targets)
            {
                character.takeDamage( _strengh );
                character.MoveBack( this.gameObject,50 );
            }
            _currentPhase ++;
            if ( _currentPhase > 2 ) _currentPhase = 0;
        }

        
    }
}