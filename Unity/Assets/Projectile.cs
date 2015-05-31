using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{

    public class Projectile : MonoBehaviour
    {
        bool move;
        public void Start()
        {
            move = true;
            Invoke( "AutoDestroy", 1f );
            GetComponentsInChildren<ParticleSystem>()[0].Play();
            GetComponentsInChildren<ParticleSystem>()[1].Play();

        }

        public void Update ()
        {
            if(move)
            {
                this.transform.transform.Translate( 0, 0, 1, Space.Self);
            }
        }

        public void OnTriggerEnter(Collider c)
        {
            Character target;
            if ( c.name != "Spi" )
            {
                target = c.GetComponent<Character>();
                if(target != null)
                {
                    move = false;
                    GetComponentsInChildren<ParticleSystem>() [1].Stop();
                    GetComponentsInChildren<ParticleSystem>() [2].Play();
                    target.takeDamage( 10 );
                }
                CancelInvoke( "AutoDestroy" );
                Destroy( this.gameObject,0.4f );
            }
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }
        
    }
}
