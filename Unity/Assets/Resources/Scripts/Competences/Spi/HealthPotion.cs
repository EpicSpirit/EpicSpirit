using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class HealthPotion : Item
    {
        public override void Awake()
        {
            base.Awake();
            _name="HealthPotion";
        }
        public override void Start ()
        {
            base.Start();
            if(_animation != null)
            {
                _attackAnimations.Add( new AttackAnimation( "HealthPotion", _animation.GetClip( "HealthPotion" ).length / 2 ) );
            }
            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
            _attackDuration = 1;
            _isStoppable = false;
        }

        public override void Act ()
        {
            base.Act();
            if ( Quantity > 0 )
            {
                _character.AnimationManager( _attackAnimations [0].AnimationName );

                _character.Health += 5;
                this.Remove();
            }
            else
            {

            }
            
        }



    }
}