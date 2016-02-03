using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class CheckPointTransition : CheckPoint
    {
        public override void OnTriggerEnter ( Collider collider )
        {
            base.OnTriggerEnter(collider);
            Application.LoadLevel( "overworld" );
        }
    }
}