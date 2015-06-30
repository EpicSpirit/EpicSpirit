using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public class ShockWave : Skill
	{
		public override void Awake ()
		{
			base.Awake();
			_cooldown = 5f ;
			_strengh = 1;


			if ( false )
			{
				_attackAnimations.Add( new AttackAnimation( "shockwave", _animations.GetClip( "shockwave" ).length * 0.6f ) );
				_attackDuration = _animations.GetClip( "shockwave" ).length;
			}
			_isStoppable = true;
		//	_image = Resources.Load<Sprite>( "UI/Images/button_shockwave" );
			_name = "Shock Wave";
			_description = "Brutal offensive spell that throw out all enemies around.";





			// Override _attackVectors
			_attackVectors = new List<Vector3>();
			_attackVectors.Add( new Vector3( 0, 1, 2 ) );
			_attackVectors.Add( new Vector3( -1, 1, 2 ) );
			_attackVectors.Add( new Vector3( 1, 1, 2 ) );
			_attackVectors.Add( new Vector3( -2, 1, 2 ) );
			_attackVectors.Add( new Vector3( 2, 1, 2 ) );
			
			_attackVectors.Add( new Vector3( 0, 0, 2 ) );
			_attackVectors.Add( new Vector3( -1, 0, 2 ) );
			_attackVectors.Add( new Vector3( 1, 0, 2 ) );
			_attackVectors.Add( new Vector3( -2, 0, 2 ) );
			_attackVectors.Add( new Vector3( 2, 0, 2 ) );
		}
		public override void CancelAttack ()
		{
			CancelAttack( "ThrowShockWave" );
			// TODO A tester pour stopper l'animation en cas de cancel.
			//_character._animations.Stop (_attackAnimations [0].AnimationName);
		}
		
		public override Action AddActionToPerso ( GameObject go )
		{
			return go.AddComponent<ShockWave>();
		}
		public override void StopAction ()
		{
			base.StopAction();
			if ( _isStoppable )
			{
				CancelInvoke();
			}
		}
		
		public override bool Act ()
		{ 
			if ( base.Act() )
			{
				//_character.AnimationManager( _attackAnimations [0].AnimationName );
				Invoke( "ThrowShockWave", _attackDuration );
				return true;
			}
			return false;

		}
		public void ThrowShockWave()
		{
			foreach(Character character in GetListOfTarget())
			{
				Debug.Log ("la");
				character.takeDamage( _strengh );
				character.MoveBack( this.gameObject,200 );
			}
		}

	}
}