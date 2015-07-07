using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class FrozenPickObject : MonoBehaviour
    {
        FrozenPick _frozenPick;

        void Start ()
        {
            Invoke( "AutoDestroy", GetComponent<Animation>().GetClip( "Take 001" ).length + 0.2f );
            _frozenPick = GameObject.Find( "ProgressionManager" ).GetComponent<FrozenPick>();
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }

        public void OnTriggerEnter ( Collider c )
        {
            if(c.tag != "Player")
            {
                c.GetComponent<Character>().takeDamage( 2, _frozenPick );
                c.GetComponent<Character>().Iced(5f);
            }
        }
    }
}