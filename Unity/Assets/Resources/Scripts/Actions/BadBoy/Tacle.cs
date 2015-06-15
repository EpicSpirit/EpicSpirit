using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EpicSpirit.Game
{
    public class Tacle : Weapon
    {

        public override void Awake ()
        {
            base.Awake();

            _attackAnimations.Add( new AttackAnimation( "tacle", _animation.GetClip( "tacle" ).length / 2 ) );
            _attackDuration = _animation.GetClip( "tacle" ).length;
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
            Invoke("Damage", _attackAnimations[0].TimeAttack);
            return true;
        }

        public void Damage ()
        {
            List<Character> targets = GetListOfTarget();
            foreach ( Character character in targets )
            {
                character.takeDamage( _strengh );
                character.MoveBack( this.gameObject, 50);
            }
        }

    }
}