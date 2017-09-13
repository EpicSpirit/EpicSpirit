using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Jail : Character
    {
        public override void Awake ()
        {
            base.Awake();
            _allowMoveBack = false;
        }
        internal override void Die ()
        {
            base.Die();
            GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoyAI>().EndOfJail();
            GameObject.Find( "VeryBadBoy" ).GetComponent<IceJail>().AvoidSpawn();
            var listJail = GameObject.FindGameObjectsWithTag( "Jail" );
            foreach(var jail in listJail)
            {
                Destroy( jail );
            }
        }

        internal override void takeDamage ( int force, Action actionAttacker )
        {
            if ( actionAttacker is FireBall )
                CurrentHealth -= force;
        }

    }
}
