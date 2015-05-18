using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class AttackAnimation
    {
        string _animationName;
        float _timeAttack;

        public float TimeAttack
        {
            get { return _timeAttack; }
        }
        public string AnimationName
        {
            get { return _animationName; }
        }

        public AttackAnimation (string a, float t)
        {
            _animationName = a;
            _timeAttack = t;
        }
    }
}