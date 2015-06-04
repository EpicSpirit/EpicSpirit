using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FireBall : Skill
    {
        public override void Awake ()
        {
            base.Awake();
            _cooldown = 5f ;
            if ( _animation != null )
            {
                _attackAnimations.Add( new AttackAnimation( "throwball", _animation.GetClip( "throwball" ).length * 0.6f ) );
                _attackDuration = _animation.GetClip( "throwball" ).length;

            }

            _image = Resources.Load<Sprite>( "UI/Images/button_fireball" );
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
                Invoke( "ThrowFireBall", _attackAnimations [0].TimeAttack );
                return true;
            }
            return false;
        }
        public void ThrowFireBall()
        {
            GameObject p = Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Prefab/Projectile" ), this.transform.position+Vector3.up, this.transform.rotation ) as GameObject;
            
        }
    }
}