using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Sword : Weapon
    {
        int _currentPhase;
        public Sword ( Character character ) : base(character)
        {
            _attackAnimations.Add( new AttackAnimation( "SimpleSword_1", 1f ) );
            _attackAnimations.Add( new AttackAnimation( "SimpleSword_2", 1f ) );
            _attackAnimations.Add( new AttackAnimation( "SimpleSword_3", 1f ) );

            _currentPhase = 0;
            _attackDuration = 1;
            _strengh = 1;
            _image = Resources.Load<Texture>("./UI/Images/SimpleSword");
            _isStoppable = false;
        }

        public override void Act ()
        {
            _character.AnimationManager( _attackAnimations[_currentPhase].AnimationName );
            Invoke( "Dammage", 1f );
        }
        public void Dammage () 
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