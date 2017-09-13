using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Split : Skill
    {
        BadBoy _badBoy1;
        BadBoy _badBoy2;
        bool _started;
        internal string _child;

        public override void Awake()
        {
            base.Awake();
            if ( _animations )
            {
                AnimationClip SplitAnimation = _animations.GetClip( "split" );
                _attackAnimations.Add( new AttackAnimation( "split", SplitAnimation.length / 2 ) );
                _attackDuration = SplitAnimation.length;
            }
            _isStoppable = false;
            _started = false;
            _child = "Characters/Prefab/BadBoySplitterLastShape";
        }

        public override void Start()
        {
            base.Start();
        }

        public void Update()
        {
            if(_started)
            {
                _badBoy1.MoveAsideTo( 3 );
                _badBoy2.MoveAsideTo( -3 );
            }
        }

        public override bool Act()
        {
            _character.AnimationManager( _attackAnimations[0].AnimationName );

            // Spawn point badBoys
            Vector3 spawnPoint1 = _character.transform.position;
            spawnPoint1.x += 0;
            Vector3 spawnPoint2 = _character.transform.position;
            spawnPoint2.x -= 0;
            
            _badBoy1 = Instantiate( UnityEngine.Resources.Load<BadBoy>( _child ), spawnPoint1, _character.transform.rotation ) as BadBoy;
            _badBoy2 = Instantiate( UnityEngine.Resources.Load<BadBoy>( _child ), spawnPoint2, _character.transform.rotation ) as BadBoy;
            _started = true;

            return true;
        }

    }
}
