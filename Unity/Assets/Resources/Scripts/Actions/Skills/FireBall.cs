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
            if ( _animations != null )
            {
                _attackAnimations.Add( new AttackAnimation( "throwball", _animations.GetClip( "throwball" ).length * 0.6f ) );
                _attackDuration = _animations.GetClip( "throwball" ).length;

            }
            _isStoppable = true;
            _image = Resources.Load<Sprite>( "UI/Images/button_fireball" );
            _name = "Fire Ball";
			_description = "Call a powerfull fire spirit wich reduces everything to ashes on its way";
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<FireBall>();
        }
        public override void StopAction ()
        {
            base.StopAction();
            if ( _isStoppable )
            {
                CancelInvoke();
            }
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
                //_character.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Audio/Effect/Fireball"));
                SoundManager.PlaySound( _character.GetComponent<AudioSource>(), SoundManager.Sound.Effect_Fireball );
                Invoke( "ThrowFireBall", _attackAnimations [0].TimeAttack );
                return true;
            }
            return false;
        }
        public void ThrowFireBall()
        {
            //GameObject p = Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Prefab/Projectile_FireBall" ), this.transform.position+Vector3.up, this.transform.rotation ) as GameObject;
            Projectile.Create( Resources.Load<GameObject>( "Prefab/Projectile_FireBall" ), this._character );
        }
    }
}