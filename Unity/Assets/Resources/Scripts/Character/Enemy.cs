using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class Enemy : Character
    {
        [SerializeField]
        bool _isSleeping;

        public bool IsSleeping
        {
            get { return _isSleeping; }
            set { _isSleeping = value; }
        }

        internal override void Die ()
        {
            base.Die();
        }
        internal virtual void Loot (UnityEngine.Object item)
        {
            Loot( item, this.transform.position, 1 );
        }

        internal virtual void Loot ( UnityEngine.Object item, Vector3 appearancePosition, int opportunity )
        {
            var num = Random.Range( 1, 100 );
            if ( num > opportunity )
            {
                var instance = Instantiate( item, appearancePosition, Quaternion.Euler(Vector3.zero));
            }
            
        }
    }
}