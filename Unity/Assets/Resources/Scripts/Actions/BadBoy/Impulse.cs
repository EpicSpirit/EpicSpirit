using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EpicSpirit.Game
{
    public class Impulse : Weapon
    {
        public override void Awake ()
        {
            base.Awake();
            if ( _animations )
            {
                AnimationClip ImpulseAnimation = _animations.GetClip( "impulse" );
                _attackAnimations.Add( new AttackAnimation( "impulse", ImpulseAnimation.length / 2 ) );
                _attackDuration = ImpulseAnimation.length;
            }
            _strengh = 1;
            _isStoppable = true;
			_range = 3;
        }

        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {
            _character.AnimationManager(_attackAnimations[0].AnimationName);
            Invoke( "Damages", _attackAnimations [0].TimeAttack );
            return true;
        }

        /// <summary>
        /// Apply damages
        /// </summary>
        private void Damages ()
        {
            foreach ( Character character in GetListOfTarget() )
            {
                character.takeDamage( _strengh );
                character.MoveBack( this.gameObject, 50);
            }
        }

    }
}