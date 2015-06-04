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

            if ( _animation != null )
            {
                _attackAnimations.Add( new AttackAnimation( "HealthPotion", _animation.GetClip( "HealthPotion" ).length / 2 ) );
                _attackDuration = _animation.GetClip( "HealthPotion" ).length;

            }

            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
            _isStoppable = false;
        }
        public override void Start ()
        {
            base.Start();
            
        }

        public override bool Act ()
        {
            if ( base.Act() )
            if ( Quantity > 0 )
            {

                _character.AnimationManager( _attackAnimations [0].AnimationName );

                _character.Health += 5;
                this.Remove();
                return true;
            }
            else
            {


                return false;
            }
            
        }



    }
}