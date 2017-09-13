using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FrozenPick : Skill
    {
        public override void Awake ()
        {
            base.Awake();
            _cooldown = 5f;
            if ( _animations != null )
            {
                _attackAnimations.Add( new AttackAnimation( "throwball", _animations.GetClip( "throwball" ).length * 0.7f ) );
                _attackDuration = _animations.GetClip( "throwball" ).length;

            }

            _image = Resources.Load<Sprite>( "UI/Images/button_frozenpick" );
            _name = "Frozen Pick";
			_description = "Summons holy ice spikes that attack with extreme cold and freeze enemies.";
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<FrozenPick>(); 
        }
       
        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {
            if ( base.Act() )
            {
                _character.AnimationManager( _attackAnimations [0].AnimationName );
                Invoke( "ThrowFrozenPick", _attackAnimations [0].TimeAttack );
                return true;
            }
            return false;
        }
        public void ThrowFrozenPick ()
        {
            Instantiate( Resources.Load<Object>( "Prefab/Frozen_Pick_Prefab" ),
                transform.position + transform.TransformDirection(Vector3.forward*8), 
                transform.rotation );
        }

        public override void CancelAttack ()
        {
            CancelAttack( "ThrowFrozenPick" );
        }
    }
}
