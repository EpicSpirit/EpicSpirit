using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ChargeTowardEnemy : Skill
    {
        VeryBadBoy _veryBadBoy;
        Character _player;

        public override void Awake ()
        {
            base.Awake();

            _attackAnimations.Add( new AttackAnimation( "charge", _animations.GetClip("charge").length ) );
            _attackDuration = _attackAnimations[0].TimeAttack;
            _strengh = 3;
            _isStoppable = false;

            _veryBadBoy = GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoy>();
            _player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>();

        }

        // Use this for initialization
        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {
            _veryBadBoy.transform.LookAt( _player.transform );
            _character.AnimationManager( _attackAnimations [0].AnimationName );
            Invoke( "Charge", _attackAnimations [0].TimeAttack );
            return true;
        }

        void Charge()
        {
            _veryBadBoy.StopMoveTo();
            _veryBadBoy.Move( _veryBadBoy.transform.TransformDirection( Vector3.forward*10 ) );

            RaycastHit raycastHit;
            Debug.DrawRay( _veryBadBoy.transform.position+(Vector3.up*2), _veryBadBoy.transform.TransformDirection( Vector3.forward )*5f, Color.blue,0.5f);
            if ( Physics.Raycast( _veryBadBoy.transform.position + ( Vector3.up * 2 ), _veryBadBoy.transform.TransformDirection( Vector3.forward ), out raycastHit, 5f ) )
            {
                Debug.Log("Touch");
                // Si on touche le joueur
                if(raycastHit.collider.tag == "Player")
                {
                    Spi spi = raycastHit.collider.GetComponent<Spi>();
                    spi.takeDamage( _strengh, this );
                    spi.MoveBack(this.gameObject,10f);
                    _veryBadBoy.EndOfState();
                    _veryBadBoy.GetComponent<VeryBadBoyAI>().EndOfCharge();

                }
                // Sinon cest que l'on touche un mur
                else
                {
                    _veryBadBoy.Stunt();
                    Invoke( "EnfOfChargeAfterStund", 2f );
                }
                CancelInvoke( "AutoStop" );
            }
            else
            {
                Invoke( "Charge", Time.deltaTime );
            }
        }

        void EnfOfChargeAfterStund()
        {
            _veryBadBoy.GetComponent<VeryBadBoyAI>().EndOfCharge();
        }

    }
}