using UnityEngine;
using System.Collections;


namespace EpicSpirit.Game
{
    public class Coffre : Enemy
    {
        public GameObject loot;
        public bool _isEmpty;

        public override void Awake ()
        {
            base.Awake();
            _isEmpty = false;
        }

        internal override void takeDamage ( int force )
        {
            if ( loot != null && !_isEmpty )
            {
                AnimationManager( "open" );
                Loot( loot, this.transform.position +this.transform.TransformDirection( Vector3.left *3), 0 );
                _isEmpty = true;
            }
            else if ( loot == null ) Debug.Log( "Pas d'objet de loot configuré sur le coffre " + this.name + " ... " );
        } 
        
    }
}