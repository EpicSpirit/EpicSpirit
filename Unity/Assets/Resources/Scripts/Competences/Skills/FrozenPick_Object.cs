using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FrozenPick_Object : MonoBehaviour
    {

        void Start ()
        {
            float t = this.GetComponent<Animation>().GetClip("Take 001").length+0.5f;
            Invoke( "AutoDestroy", t );
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }

        public void OnTriggerEnter ( Collider c )
    {
        if(c.name != "Spi")
        {
            c.GetComponent<Character>().takeDamage( 10 );
        }
    }
    }
}