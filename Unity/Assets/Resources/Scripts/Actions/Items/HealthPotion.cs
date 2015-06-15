using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class HealthPotion : Item
    {
        int _healingAmount;

        /// <summary>
        /// Amount of HP recovered by using method Act
        /// </summary>
        public int HealingAmount
        {
            get { return _healingAmount; }
            set { _healingAmount = value; }
        }

        public override void Awake()
        {
            base.Awake();
            _name="Health Potion";
			_description = "Restaure a swall amount of hp.";
            HealingAmount = 1;


            if ( _animation != null )
            {
                _attackAnimations.Add( new AttackAnimation( "HealthPotion", _animation.GetClip( "HealthPotion" ).length / 2 ) );
                _attackDuration = _animation.GetClip( "HealthPotion" ).length;

            }

            _image = Resources.Load<Sprite>( "UI/Images/button_health_potion" );
            _isStoppable = false;
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<HealthPotion>();
        }

        public override void Start ()
        {
            base.Start();
            
        }

        public override bool Act ()
        {
            base.Act();
            if ( Quantity > 0 )
            {
                _character.AnimationManager( _attackAnimations [0].AnimationName );

                _character.Health += HealingAmount;
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