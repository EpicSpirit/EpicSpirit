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

        public enum FigthPhaseEnum
        {
            Wait,
            Protection

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

        }
        public void Figth()
        {
            Debug.Log("Figth !");
            figthPhase = FigthPhaseEnum.Protection;
        }
        public override void Update ()
        {
            if(figthPhase == FigthPhaseEnum.Protection)
            {
                PhaseGoToProtection();
                figthPhase = FigthPhaseEnum.Wait;
            }
        }

        private void PhaseGoToProtection()
        {
            _character.MoveTo( _centralPoint.position, TurnAroundAttack );
        }

        public void TurnAroundAttack()
        {
            Debug.Log("TurnAroundAttack");
            _veryBadBoy.Attack((int)Attack.TurnAround);

        }
	    
	}
}