using UnityEngine;
using System.Collections;
using System;

namespace EpicSpirit.Game
{
    public class HealthPotion : Item
    {
        byte _healingAmount;

        /// <summary>
        /// Amount of HP recovered by using method Act
        /// </summary>
        public byte HealingAmount
        {
            get { return _healingAmount; }
            set 
            {
                if ( value > 124 )
                    throw new ArgumentException( "Too much HP for a byte" );
                _healingAmount = value; 
            }
        }

        public override void Awake()
        {
            base.Awake();
            _name="Health Potion";
			_description = "Restaure a swall amount of hp.";
            HealingAmount = 1;

            if ( _animations != null )
            {
                AnimationClip HealthPotionAnimation = _animations.GetClip( "HealthPotion" );

                _attackAnimations.Add( new AttackAnimation( "HealthPotion", HealthPotionAnimation.length / 2 ) );
                _attackDuration = HealthPotionAnimation.length;
            }

            _image = Resources.Load<Sprite>( "UI/Images/Potion2" );
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
            if ( Quantity > 0 )
            {
                base.Act();
                _character.AnimationManager( _attackAnimations [0].AnimationName );
                _character.CurrentHealth += HealingAmount;
                this.Remove();
                GameObject.Find( "Item" ).GetComponentInChildren<UIItem>().UpdateCount();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}