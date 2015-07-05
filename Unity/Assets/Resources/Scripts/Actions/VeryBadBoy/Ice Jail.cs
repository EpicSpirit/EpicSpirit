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
        }

        public override bool Act ()
        {
            _character.AnimationManager( _attackAnimations [0].AnimationName );
            Invoke( "CreateJail", _attackAnimations [0].TimeAttack );
            return true;
        }

        public void CreateJail ()
        {
            Instantiate(Resources.Load<GameObject>("Prefab/Jail"), _character.transform.position, _character.transform.rotation);

            var listSpawner = GameObject.Find( "JailSpawner" ).GetComponentsInChildren<CinematicSpawnPoint>();
            foreach(var spawner in listSpawner)
            {
                spawner.Spawn().GetComponent<AIController>().Target = _player.gameObject;
            }

        }

    }
}