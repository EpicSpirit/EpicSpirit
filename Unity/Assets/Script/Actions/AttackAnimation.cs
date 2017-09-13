using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public struct AttackAnimation
    {
        string _attackAnimationName;
        float _timeToWaitBeforeHit;

        public float TimeAttack
        {
            get { return _timeToWaitBeforeHit; }
        }
        public string AnimationName
        {
            get { return _attackAnimationName; }
        }

        public AttackAnimation (string attackAnimationName, float timeToWaitBeforeHit)
        {
            _attackAnimationName = attackAnimationName;
            _timeToWaitBeforeHit = timeToWaitBeforeHit;
        }
    }
}
