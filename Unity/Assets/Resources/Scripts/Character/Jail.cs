using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Jail : Character
    {
        public override void Awake ()
        {
            base.Awake();
        }
        internal override void Die ()
        {
            base.Die();
            GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoyAI>().EndOfJail();
        }
    }
}