using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Dodge : Skill
    {
        public override void Awake ()
        {
            base.Awake();
            _cooldown = 1f;
            if ( _animation != null )
            {
                _attackAnimations.Add( new AttackAnimation( "dodge", 0f ) );
                _attackDuration = _animation.GetClip( "dodge" ).length;
            }
            _isStoppable = false;
            _image = Resources.Load<Sprite>( "UI/Images/button_retreat" );

            _name = "Retreat";
			_description = "Jump out from the battle quickly.";
        }

        public override Action AddActionToPerso ( GameObject go )
        {
            return go.AddComponent<Dodge>();
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

                Invoke( "UpdatePosition", 0f );
                Invoke( "StopUpdatePosition", _attackDuration );
                return true;
            }
            return false;
        }


        public void UpdatePosition ()
        {
            _character.MoveBack( 8 );
            Invoke( "UpdatePosition", _attackDuration/100 );

        }
        public void StopUpdatePosition ()
        {
            CancelInvoke( "UpdatePosition" );
        }
    }
}
