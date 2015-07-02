using UnityEngine;
using System.Collections;


namespace EpicSpirit.Game
{
    public class Coffre : Enemy
    {
        public GameObject loot;

        public override void Awake ()
        {
            base.Awake();
        }

        internal override void takeDamage ( int force )
        {
            AnimationManager( "open" );
            if ( loot != null )
                Loot( loot, this.transform.TransformDirection( Vector3.forward ), 100 );
            else Debug.Log("Pas d'objet de loot configuré sur le coffre "+this.name+" ... ");
        } 
        
    }
}