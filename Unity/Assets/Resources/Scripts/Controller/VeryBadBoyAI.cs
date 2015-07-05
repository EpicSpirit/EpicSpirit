using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class VeryBadBoyAI : AIController 
   	{
        Transform _centralPoint;
        public bool isInvincible;
        FigthPhaseEnum figthPhase;
        VeryBadBoy _veryBadBoy;
        int _bouclier;
        int _numberOfCharge;

        public enum FigthPhaseEnum
        {
            Wait,
            Protection,
            Charge,
            GoToJail
        }

        public enum Attack :int
        {
            SummonBadBoy=0,
            TurnAround=1,
            IceJail=2,
            ChargeTowardEnemy=3
        }

        internal override void Awake()
        {
            base.Awake();
            _centralPoint = GameObject.Find( "CentralPoint" ).transform;
            isInvincible = true;
            figthPhase = FigthPhaseEnum.Wait;
            _character._movementSpeed = 15;
            _veryBadBoy = (VeryBadBoy)_character;
            _bouclier = 0;
            _numberOfCharge = 0;

        }
        public void Figth()
        {
            figthPhase = FigthPhaseEnum.Protection;
        }
        public override void Update ()
        {
            if(figthPhase == FigthPhaseEnum.Protection)
            {
                PhaseGoToProtection();
                figthPhase = FigthPhaseEnum.Wait;
            }
            if(figthPhase == FigthPhaseEnum.Charge)
            {
                Charge();
            }
        }

        private void Charge ()
        {
            _veryBadBoy.Attack( (int)Attack.ChargeTowardEnemy );
            figthPhase = FigthPhaseEnum.Wait;
            isInvincible = false;
        }

        public void EndOfCharge()
        {
            _numberOfCharge++;
            if ( _numberOfCharge < 3 )
            {
                Invoke( "ChangeStateToCharge", 1f );
            }
            else
            {
                Invoke( "ChangeStateToProtection", 1f );
            }
        }
        public void ChangeStateToCharge()
        {
            figthPhase = FigthPhaseEnum.Charge;
        }
        public void ChangeStateToProtection ()
        {
            figthPhase = FigthPhaseEnum.Protection;
        }

        private void PhaseGoToProtection()
        {
            isInvincible = true;
            _character.MoveTo( _centralPoint.position, TurnAroundAttack );
        }

        public void TurnAroundAttack()
        {
            Debug.Log("TurnAroundAttack");
            _veryBadBoy.Attack((int)Attack.TurnAround);
            _bouclier = 3;
        }

        internal void SubBadBoyBouclier ()
        {
            _bouclier--;
            if(_bouclier==0)
            {
                _numberOfCharge = 0;
                figthPhase = FigthPhaseEnum.Charge;
            }
        }

    }
}