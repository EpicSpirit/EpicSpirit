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
            Loot( item, this.transform.position, 10 );
        }

        internal virtual void Loot ( UnityEngine.Object item, Vector3 appearancePosition, int lootRate )
        {//Je pense que tu voulais mettre lootRate au lieu de opportunity, mais j'suis pas trop sur alors je commente
            var num = Random.Range( 1, 100 );
            if ( num < lootRate )
            {
                var instance = Instantiate( item, appearancePosition, Quaternion.Euler(Vector3.zero));
            }
            
        }

    }
}