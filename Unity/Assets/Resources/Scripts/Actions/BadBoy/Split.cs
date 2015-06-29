using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Split : Skill
    {
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
        }

        public override void Start()
        {
            base.Start();
        }

        public override bool Act()
        {
            _character.AnimationManager( _attackAnimations[0].AnimationName );

            // Spawn point badBoys
            Vector3 spawnPoint1 = _character.transform.position;
            spawnPoint1.x += 4;
            Vector3 spawnPoint2 = _character.transform.position;
            spawnPoint2.x -= 4;

            Object badBoy1 = Instantiate( (UnityEngine.Object)UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/BadBoy" ), spawnPoint1, _character.transform.rotation );
            Object badBoy2 = Instantiate( (UnityEngine.Object)UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/BadBoy" ), spawnPoint2, _character.transform.rotation );
            
            return true;
        }

    }
}