using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class IceJail : Skill
    {
        VeryBadBoy _veryBadBoy;
        Character _player;

        public override void Awake ()
        {
            base.Awake();

            _attackAnimations.Add( new AttackAnimation( "jail", _animations.GetClip("jail").length ) );
            _attackDuration = _attackAnimations [0].TimeAttack;
            _strengh = 0;
            _isStoppable = false;

            _player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>();
            _veryBadBoy = GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoy>();
        }

        public override bool Act ()
        {
            _character.AnimationManager( _attackAnimations [0].AnimationName );
            Invoke( "CreateJail", _attackAnimations [0].TimeAttack );
            return true;
        }

        public void CreateJail ()
        {
            var listSpawerJail = GameObject.Find( "JailSpawnerJails" ).GetComponentsInChildren<CinematicSpawnPoint>();
            foreach ( var spawner in listSpawerJail )
            {
                var jail = spawner.Spawn();
                jail.transform.LookAt( _veryBadBoy.transform.position);
            }
            Invoke( "SpawnAll", 3f );
			Invoke ("EndJail", 3f);
        }

		public void EndJail()
		{
			var listJail = GameObject.FindGameObjectsWithTag( "Jail" );
			foreach(var jail in listJail)
			{
				Destroy( jail );
			}
			GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoyAI>().EndOfJail();

		}

        public void AvoidSpawn()
        {
            CancelInvoke( "SpawnAll" );
        }

        public void SpawnAll()
        {
            var listSpawner = GameObject.Find( "JailSpawner" ).GetComponentsInChildren<CinematicSpawnPoint>();
            foreach ( var spawner in listSpawner )
            {
                spawner.Spawn().GetComponent<AIController>().Target = _player.gameObject;
            }
        }

    }
}