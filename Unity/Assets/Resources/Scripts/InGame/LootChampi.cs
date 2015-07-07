using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class LootChampi : Loot
    {
        public override void Awake()
        {
            base.Awake();
            CancelInvoke( "Extinction" );
        }

        public override void OnTriggerStay ( Collider collider )
        {
            if(collider.tag == "Player")
            {
                var meshRenderer = GameObject.Find("Group33316").GetComponent<SkinnedMeshRenderer>();
                meshRenderer.material = Resources.Load<Material>( "Characters/Modele/Spi/DarkBody" );

                meshRenderer = GameObject.Find( "Sphere005" ).GetComponent<SkinnedMeshRenderer>();
                meshRenderer.material = Resources.Load<Material>( "Characters/Modele/Spi/DarkEyes" );

                Extinction();
            }
        }
        
    }
}