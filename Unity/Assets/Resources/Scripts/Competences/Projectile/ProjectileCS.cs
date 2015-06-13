using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	// Que veux dire CS ?
    public abstract class ProjectileCS : MonoBehaviour
    {
        public virtual void Awake()
        {
            Invoke( "AutoDestroy", 1f );
            GetComponentsInChildren<ParticleSystem>()[0].Play();
            GetComponentsInChildren<ParticleSystem>()[1].Play();

        }

        public virtual void Update ()
        {
            this.transform.transform.Translate( 0, 0, 1, Space.Self);
        }

        public void OnTriggerEnter(Collider c)
        {
            Character target;
            if ( c.name != "Spi" )
            {
                target = c.GetComponent<Character>();
                if(target != null)
                {
                    Effect( target );
                }
                CancelInvoke( "AutoDestroy" );
                Destroy( this.gameObject,0.15f );
            }
        }

        public void AutoDestroy ()
        {
            Destroy( this.gameObject );
        }

        public abstract void Effect (Character target);
        
    }
}
