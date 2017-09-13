using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class TurnAround : Skill
    {
        Transform[] listPoint;
        int indexPoint;

        public override void Awake ()
        {
            base.Awake();
            _attackAnimations.Add( new AttackAnimation( "invoke", _animations.GetClip( "invoke" ).length / 2 ) );
            _attackDuration = _animations.GetClip( "invoke" ).length;
            _strengh = 0;
            _isStoppable = true;
            listPoint = GameObject.Find( "PointMouvement" ).GetComponentsInChildren<Transform>();
            indexPoint = 1;
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

            Invoke("NextPoint", 1f);
        }

        void InvokeBadBoy()
        {
            GameObject.Find( "SpawnerTurnAround" ).GetComponent<CinematicSpawnPoint>().Spawn().GetComponent<AIController>().Target = GameObject.FindGameObjectWithTag( "Player" );

        }    

        void NextPoint()
        {
            _character.StopMoveTo();
            _character.MoveTo( listPoint [indexPoint].position, NextPoint);
            indexPoint++;
            InvokeBadBoy();
            if ( indexPoint == 5 )
            {
                indexPoint = 1;
            }
        }
    }
}
