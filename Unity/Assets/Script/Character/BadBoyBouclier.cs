using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoyBouclier : Character
    {
        VeryBadBoyAI _veryBadBoy;
        public override void Awake()
        {
            base.Awake();
            _currentHealth = 1;
            _veryBadBoy = GameObject.Find( "VeryBadBoy" ).GetComponent<VeryBadBoyAI>();
        }

        internal override void Die ()
        {
            base.Die();
            _veryBadBoy.SubBadBoyBouclier();

        }
        
    }
}
