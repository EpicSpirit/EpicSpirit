using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class SummonBadBoy : Skill
    {

        // Use this for initialization
        public override void Start ()
        {
            base.Start();

            _attackAnimations.Add( new AttackAnimation( "invoke" , _animation.GetClip("invoke").length/2) );
            _attackDuration = _animation.GetClip( "invoke" ).length;
            _strengh = 0;
            _isStoppable = false;
        }

        public override void Act ()
        {
            
        }

        
    }

}