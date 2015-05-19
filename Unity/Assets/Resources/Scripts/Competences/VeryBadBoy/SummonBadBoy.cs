using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class SummonBadBoy : Skill
    {
        private static System.Random _randomGenerator;

        // Use this for initialization
        public override void Start ()
        {
            base.Start();

            _randomGenerator = new System.Random();
            _attackAnimations.Add( new AttackAnimation( "invoke" , _animation.GetClip("invoke").length/2) );
            _attackDuration = _animation.GetClip( "invoke" ).length;
            _strengh = 0;
            _isStoppable = false;
        }

        public override void Act ()
        {
            _character.AnimationManager(_attackAnimations[0].AnimationName);
            Invoke("Invoke", _attackAnimations[0].TimeAttack);
        }

        public void Invoke () 
        {
            // Real attack
            
            Vector3 position = _character.transform.position;

            position.x += _randomGenerator.Next( 1, 5 );
            position.z += _randomGenerator.Next( 1, 5 );

            GameObject badBoy = Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "Characters/Prefab/BadBoy" ), position, this.transform.rotation ) as GameObject;
            Character enemy = badBoy.GetComponent<Character>();
            enemy.ParticuleManager( "Invokation" );


        }
    }
}