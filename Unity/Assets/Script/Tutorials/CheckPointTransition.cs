using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace EpicSpirit.Game
{
    public class CheckPointTransition : CheckPoint
    {
        public override void OnTriggerEnter ( Collider collider )
        {
            base.OnTriggerEnter(collider);
            SceneManager.LoadScene( "overworld" );
        }
    }
}