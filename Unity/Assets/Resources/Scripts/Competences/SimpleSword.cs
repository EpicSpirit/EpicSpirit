using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
    public class Sword : Weapon
    {
        public Sword ( Character character ):base(character)
        {
            var a = _character.GetComponentInChildren<Animation>();
            _animations.Add( a.GetClip( "SimpleSword_1" ) );
            _animations.Add( a.GetClip( "SimpleSword_2" ) );
            _animations.Add( a.GetClip( "SimpleSword_3" ) );

            _attackDuration = 1;
            _strengh = 1;
            _image = Resources.Load<Texture>("./UI/Images/SimpleSword");
            _isStoppable = false;
        }

        public override void Act ()
        {
            // Phase 1 : Play the animation
            _character.AnimationManager( "SimpleSword_1" );

            List<Character> targets = GetListOfTarget();
            foreach(Character character in targets)
            {
                character.takeDamage( _strengh );
            }
        }
    }
}