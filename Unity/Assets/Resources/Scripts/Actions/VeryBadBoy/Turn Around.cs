using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class TurnAround : Skill
    {
        public override void Awake ()
        {
            base.Awake();
            _attackAnimations.Add( new AttackAnimation( "invoke", _animations.GetClip( "invoke" ).length / 2 ) );
            _attackDuration = _animations.GetClip( "invoke" ).length;
            _strengh = 0;
            _isStoppable = false;
        }

        // Use this for initialization
        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {

            _character.AnimationManager( _attackAnimations [0].AnimationName );
            Invoke( "Invoke", _attackAnimations [0].TimeAttack );
            return true;

        }

        public void Invoke ()
        {
            var pos1 = GameObject.Find( "pos1" );
            var pos2 = GameObject.Find( "pos2" );
            var pos3 = GameObject.Find( "pos3" );
            GameObject go;

            go = (GameObject)Instantiate( Resources.Load<GameObject>( "Characters/Prefab/BadBoyBouclier" ), pos1.transform.position, pos1.transform.rotation );
            go.transform.parent = pos1.transform;
            go = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Characters/Prefab/BadBoyBouclier" ), pos2.transform.position, pos2.transform.rotation );
            go.transform.parent = pos2.transform;

            go = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Characters/Prefab/BadBoyBouclier" ), pos3.transform.position, pos3.transform.rotation );
            go.transform.parent = pos3.transform;


        }
    }
}