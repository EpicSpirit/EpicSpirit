using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FrozenPickObject : MonoBehaviour
    {
        void Start ()
        {
            Invoke( "AutoDestroy", GetComponent<Animation>().GetClip( "Take 001" ).length + 0.2f );
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }

        public void OnTriggerEnter ( Collider c )
        {
            if(c.name != "Spi")
            {
                c.GetComponent<Character>().takeDamage( 2 );
                c.GetComponent<Character>().Iced(5f);
            }
        }
    }
}