using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Enemy : Character
    {
        internal override void Die ()
        {
            base.Die();
        }
        internal virtual void Loot (UnityEngine.Object item)
        {
            var num = Random.Range( 1, 100 );
            if ( num > 50 )
                Instantiate( item, this.transform.position, this.transform.rotation );
        }
    }
}