using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ChargeTowardEnemy : Skill
    {
        VeryBadBoy _veryBadBoy;
        Character _player;
        bool stop;

        public override void Awake ()
        {
            base.Awake();
            stop = false;
            Invoke( "AutoStop", 10f );

            _attackAnimations.Add( new AttackAnimation( "charge", 0 ) );
            _attackDuration = 1f;
            _strengh = 3;
            _isStoppable = false;

            _veryBadBoy = GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoy>();
            _player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>();

        }
        void AutoStop()
        {
            Debug.Log("AttoStop");
            stop = true;
            _veryBadBoy.GetComponent<VeryBadBoyAI>().EndOfCharge();

        }

        // Use this for initialization
        public override void Start ()
        {
            base.Start();
        }

        public override bool Act ()
        {
            _veryBadBoy.transform.LookAt( _player.transform );
            Invoke( "Charge", 0.5f );
            return true;

        }

        void Charge()
        {
            _veryBadBoy.Move( _veryBadBoy.transform.TransformDirection( Vector3.forward*2 ) );

            RaycastHit raycastHit;
            Debug.DrawRay( _veryBadBoy.transform.position+(Vector3.up*2), _veryBadBoy.transform.TransformDirection( Vector3.forward )*5f, Color.blue,0.5f);
            if ( Physics.Raycast( _veryBadBoy.transform.position + ( Vector3.up * 2 ), _veryBadBoy.transform.TransformDirection( Vector3.forward ), out raycastHit, 5f ) )
            {
                // Si on touche le joueur
                if(raycastHit.collider.tag == "Player")
                {
                    raycastHit.collider.GetComponent<Spi>().takeDamage( _strengh, this );
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
            else if(!stop) 
            {
                Invoke( "Charge", Time.deltaTime );
            }
            else
            { Debug.Log("Normalement je ne passe jamais dedans"); }
        }

        void EnfOfChargeAfterStund()
        {
            _veryBadBoy.GetComponent<VeryBadBoyAI>().EndOfCharge();
        }

    }
}