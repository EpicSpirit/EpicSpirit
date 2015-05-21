using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FireBall : Skill
    {
        public override void Start ()
        {
            base.Start();

            _image = Resources.Load<Sprite>( "UI/Images/button_fireball" );
            _attackDuration = 1f;
        }

        public override void Act ()
        {
            Debug.Log( "SUPER FIREBALL !" );
        }
    }
}