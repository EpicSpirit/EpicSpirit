using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class HealthPotion : Item
    {
        public override void Start ()
        {
            base.Start();

            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
            _attackDuration = 1;
            _isStoppable = false;
        }

        public override void Act ()
        {
            _character.Health += 5;
        }

    }
}