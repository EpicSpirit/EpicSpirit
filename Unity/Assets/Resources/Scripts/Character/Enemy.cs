using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Enemy : Character
    {
        internal override void Die ()
        {
            var num = Random.Range( 1, 100 );
            if ( num > 80 )
                Instantiate( ( UnityEngine.Object ) UnityEngine.Resources.Load<UnityEngine.Object>( "CollectableItem/HealthPotion_Item" ), this.transform.position, this.transform.rotation );

            base.Die();
        }

    }
}