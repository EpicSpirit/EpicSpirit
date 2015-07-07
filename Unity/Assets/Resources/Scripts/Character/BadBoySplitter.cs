using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class BadBoySplitter : BadBoy
    {
        public override void Awake()
        {
            base.Awake();

            _currentHealth = 3;
            _actions.Add( this.gameObject.AddComponent<Split>() );

        }
        internal override void Die()
        {
            State = States.Idle;
            Attack( 1 );
            base.Die();
        }
        internal override void Loot( Object item )
        {
            // Don't loot
        }
    }
}