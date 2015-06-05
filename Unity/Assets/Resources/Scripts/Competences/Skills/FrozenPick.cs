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
            if ( _animation != null )
            {
                _attackAnimations.Add( new AttackAnimation( "throwball", _animation.GetClip( "throwball" ).length * 0.6f ) );
                _attackDuration = _animation.GetClip( "throwball" ).length;

            }

            _image = Resources.Load<Sprite>( "UI/Images/button_frozenpick" );
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
            GameObject p = Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Prefab/Projectile_FrozenPick" ), this.transform.position + Vector3.up, this.transform.rotation ) as GameObject;

        }
    }
}
